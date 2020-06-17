using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager active;
    public List<AudioClip> audioClips;
    AudioSource audioSource;

    int CurrentClip = 0;
    public int currentClip
    {
        get => CurrentClip;
        set
        {
            if (value >= 0 && value < audioClips.Count)
                CurrentClip = value;
            else CurrentClip = 0;
            audioSource.clip = audioClips[CurrentClip];
            audioSource.Play();
        }
    }

    void Start()
    {
        active = this;
        audioSource = GetComponent<AudioSource>();
        currentClip = 0;
    }

    void FixedUpdate()
    {
        if (!audioSource.isPlaying)
            currentClip++;
    }
}
