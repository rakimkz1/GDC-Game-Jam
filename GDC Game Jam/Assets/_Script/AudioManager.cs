using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource backGround;
    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        Settings.musicVolumeChanged += SetMusicVolume;
    }
    public void SetMusicVolume()
    {
        backGround.volume = Settings.instance.musicVolume;
    }

    public void Play(AudioClip audio, float volume)
    {
        source.volume = volume * Settings.instance.soundVolume;
        source.PlayOneShot(audio);
    }

    public void Play(AudioClip audio)
    {
        Play(audio, 1f);
    }
}
