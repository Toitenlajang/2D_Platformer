using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Soundtype
{
    FOOTSTEPS,
    JUMP,
    LAND,
    PUNCH,
    SWORD,
    HURT,
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager Instance;

    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;

    [SerializeField]
    private Slider sfxSlider; 

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomSound(soundName);

        if(audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }
}
