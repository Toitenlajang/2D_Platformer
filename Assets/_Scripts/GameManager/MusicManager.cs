using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;

    [Header("Music Settings")]
    [SerializeField] private AudioClip defaultMusic;
    [SerializeField] private bool loopMusic = true;
    [SerializeField] private float fadeSpeed = 1f;

    [Header("UI References")]
    [SerializeField] private string musicSliderTag = "MusicSlider"; // Optional: use tag to find slider
    private Slider currentMusicSlider;

    private float targetVolume = 1f;
    private float currentVolume = 1f;
    private bool isFading = false;

    private void Awake()
    {
        // Singleton pattern with DontDestroyOnLoad
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.loop = loopMusic;
            audioSource.playOnAwake = false;

            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Load saved volume preference
        currentVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        targetVolume = currentVolume;
        audioSource.volume = currentVolume;

        // Setup initial slider
        FindAndSetupSlider();

        // Play default music if assigned
        if (defaultMusic != null)
        {
            PlayBackgroundMusic(defaultMusic, false);
        }
    }
    public void Update()
    {
        // Smooth volume fading
        if (isFading)
        {
            currentVolume = Mathf.Lerp(currentVolume, targetVolume, Time.deltaTime * fadeSpeed);
            audioSource.volume = currentVolume;

            if (Mathf.Abs(currentVolume - targetVolume) < 0.01f)
            {
                currentVolume = targetVolume;
                audioSource.volume = currentVolume;
                isFading = false;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find slider in new scene
        FindAndSetupSlider();

        // Check for scene-specific music
        SceneMusicData musicData = FindObjectOfType<SceneMusicData>();
        if (musicData != null && musicData.sceneMusic != null)
        {
            PlayBackgroundMusic(musicData.sceneMusic, true, musicData.fadeIn);
        }
    }

    private void FindAndSetupSlider()
    {
        // Try to find slider by tag first
        GameObject sliderObj = GameObject.FindGameObjectWithTag(musicSliderTag);
        if (sliderObj != null)
        {
            currentMusicSlider = sliderObj.GetComponent<Slider>();
        }

        // Fallback: search in all canvases
        if (currentMusicSlider == null)
        {
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                Slider[] sliders = canvas.GetComponentsInChildren<Slider>(true);
                foreach (Slider slider in sliders)
                {
                    // You can add naming convention check here
                    if (slider.gameObject.name.Contains("Music"))
                    {
                        currentMusicSlider = slider;
                        break;
                    }
                }
                if (currentMusicSlider != null) break;
            }
        }

        // Setup slider if found
        if (currentMusicSlider != null)
        {
            currentMusicSlider.value = currentVolume;
            currentMusicSlider.onValueChanged.RemoveAllListeners();
            currentMusicSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Public static method to set volume from anywhere
    public static void SetVolume(float volume)
    {
        if (Instance != null)
        {
            Instance.targetVolume = Mathf.Clamp01(volume);
            Instance.currentVolume = Instance.targetVolume;
            Instance.audioSource.volume = Instance.currentVolume;

            // Save preference
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
    }

    // Play music with optional crossfade
    public static void PlayBackgroundMusic(AudioClip music, bool restart = false, bool fade = false)
    {
        if (Instance != null)
        {
            Instance.PlayMusicInternal(music, restart, fade);
        }
    }

    private void PlayMusicInternal(AudioClip music, bool restart, bool fade)
    {
        if (music == null) return;

        // If same music is already playing and not restarting, do nothing
        if (audioSource.clip == music && audioSource.isPlaying && !restart)
        {
            return;
        }

        if (fade)
        {
            StartCoroutine(CrossfadeMusic(music));
        }
        else
        {
            audioSource.clip = music;
            audioSource.Play();
        }
    }

    private IEnumerator CrossfadeMusic(AudioClip newMusic)
    {
        // Fade out current music
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.01f)
        {
            audioSource.volume -= startVolume * Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Switch music
        audioSource.Stop();
        audioSource.clip = newMusic;
        audioSource.Play();

        // Fade in new music
        audioSource.volume = 0f;
        while (audioSource.volume < targetVolume - 0.01f)
        {
            audioSource.volume += targetVolume * Time.deltaTime * fadeSpeed;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    // Pause music
    public static void PauseMusic()
    {
        if (Instance != null && Instance.audioSource.isPlaying)
        {
            Instance.audioSource.Pause();
        }
    }

    // Resume music
    public static void ResumeMusic()
    {
        if (Instance != null && !Instance.audioSource.isPlaying)
        {
            Instance.audioSource.UnPause();
        }
    }

    // Stop music completely
    public static void StopMusic(bool fade = false)
    {
        if (Instance != null)
        {
            if (fade)
            {
                Instance.StartCoroutine(Instance.FadeOutAndStop());
            }
            else
            {
                Instance.audioSource.Stop();
            }
        }
    }

    private IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.01f)
        {
            audioSource.volume -= startVolume * Time.deltaTime * fadeSpeed;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = targetVolume;
    }

    // Check if music is playing
    public static bool IsPlaying()
    {
        return Instance != null && Instance.audioSource.isPlaying;
    }

    // Get current playing clip
    public static AudioClip GetCurrentClip()
    {
        return Instance != null ? Instance.audioSource.clip : null;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
