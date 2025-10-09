using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;

public enum Soundtype
{
    // Player Sounds
    PLAYER_FOOTSTEPS,
    PLAYER_JUMP,
    PLAYER_LAND,
    PLAYER_DASH,
    PLAYER_CLIMB,
    PLAYER_SLIDE,
    PLAYER_PUNCH,
    PLAYER_SWORD,
    PLAYER_HURT,
    PLAYER_DEAD,

    // Enemy Sounds

    // Item Sounds

    // UI Sounds
}
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
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
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        instance.audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(Soundtype));
        Array.Resize(ref soundList, names.Length);
        for(int i =0; i < soundList.Length ; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif

    [Serializable]
    public struct SoundList
    {
        public AudioClip[] Sounds { get => sounds; }

        [HideInInspector] public string name;
        [SerializeField] private AudioClip[] sounds;
    }
}
