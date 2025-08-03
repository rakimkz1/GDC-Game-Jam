using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip audio, float volume)
    {
        source.volume = volume;
        source.PlayOneShot(audio);
    }

    public void Play(AudioClip audio)
    {
        Play(audio, 1f);
    }
}
