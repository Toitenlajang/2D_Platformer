using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicData : MonoBehaviour
{
    [Header("Scene Music Settings")]
    [Tooltip("The music to play in this scene")]
    public AudioClip sceneMusic;

    [Tooltip("Fade in when switching to this music")]
    public bool fadeIn = true;

    [Tooltip("Auto-play music when scene loads")]
    public bool autoPlay = true;

    [Tooltip("Force restart even if same music is playing")]
    public bool forceRestart = false;

    private void Awake()
    {
        // Play music as early as possible
        if (autoPlay && sceneMusic != null)
        {
            MusicManager.PlayBackgroundMusic(sceneMusic, forceRestart, fadeIn);
        }
    }
}
