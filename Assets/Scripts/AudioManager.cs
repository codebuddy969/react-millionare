using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public GameObject document;

    public SoundsList[] sounds;

    DatabaseManager databaseManager;

    UIList interfaces;

    GameDataConfig gameDataConfig;
    
    public static AudioManager manager;
    void Awake()
    {
        manager = this;
        // if (manager == null) {
        //     manager = this;
        // } else {
        //     Destroy(gameObject);
        //     return;
        // }

        // DontDestroyOnLoad(gameObject);

        foreach (SoundsList s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Start()
    {
        interfaces = new UIList(document, SceneManager.GetActiveScene().name == "Game");

        databaseManager = DatabaseManager.manager;

        gameDataConfig = databaseManager.LoadSaving();

        musicVolume(gameDataConfig.musicVolume);
        effectsVolume(gameDataConfig.effectsVolume);
    }

    public void Play(string name)
    {
        SoundsList s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        SoundsList s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        s.source.Stop();
    }

    public void StopAndPlay(string[] stop_sounds, string[] start_sounds)
    {
        foreach(string name in stop_sounds) {
            SoundsList s = Array.Find(sounds, sound => sound.name == name);
            if (s == null) {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
            s.source.Stop();
        }

        foreach(string name in start_sounds) {
            SoundsList s = Array.Find(sounds, sound => sound.name == name);
            if (s == null) {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
            s.source.Play();
        }
    }

    public void StopMultiple(string[] stop_sounds) 
    {
        foreach(string name in stop_sounds) {
            SoundsList s = Array.Find(sounds, sound => sound.name == name);
            if (s == null) {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
            s.source.Stop();
        }
    }

    public void musicVolume(float volume)
    {
        foreach (SoundsList s in sounds)
        {
            if (s.music)
            {
                s.source.volume = (float)(volume / 3);
            }
        }

        interfaces.musicSlider.GetComponent<Slider>().value = volume;

        gameDataConfig.musicVolume = volume;

        databaseManager.CreateSaving(gameDataConfig);
    }

    public void effectsVolume(float volume)
    {
        foreach (SoundsList s in sounds)
        {
            if (!s.music)
            {
                s.source.volume = (float)(volume / 3);
            }
        }

        interfaces.effectsSlider.GetComponent<Slider>().value = volume;

        gameDataConfig.effectsVolume = volume;

        databaseManager.CreateSaving(gameDataConfig);
    }

    public void PlayRandomTrack()
    {
        int index = UnityEngine.Random.Range(1, 3);

        Play("menu-" + index);
    }
}
