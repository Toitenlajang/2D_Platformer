using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Soundtype
{
    FOOTSTEPS,
    JUMP,
    LAND,
    DASH,
    CLIMB,
    SLIDE,
    PUNCH,
    SWORD,
    HURT,
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static void Playsound(Soundtype sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
    
}
