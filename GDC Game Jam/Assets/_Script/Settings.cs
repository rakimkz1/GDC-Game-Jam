using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    public static event Action musicVolumeChanged;
    public static event Action soundVolumeChanged;
    
    public Slider musicSlider;
    public Slider soundSlider;

    public string musicName = "Music";
    public string soundName = "Sound";
    
    public float musicVolume = 1.0f;
    public float soundVolume = 1.0f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {   
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMusic);
        soundSlider.onValueChanged.AddListener(SetSound);
        musicVolume = PlayerPrefs.GetFloat(musicName, 1.0f);
        soundVolume = PlayerPrefs.GetFloat(soundName, 1.0f);
        
        musicSlider.value = musicVolume;
        soundSlider.value = soundVolume;
    }

    public void SetMusic(float value)
    {
        PlayerPrefs.SetFloat(musicName, value);
        musicVolume = value;
        musicVolumeChanged?.Invoke();
    }

    public void SetSound(float value)
    {
        PlayerPrefs.SetFloat(soundName, value);
        soundVolume = value;
        soundVolumeChanged?.Invoke();
    }
}
