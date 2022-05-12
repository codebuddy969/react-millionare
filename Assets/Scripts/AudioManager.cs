using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    private GameDataConfig saving;

    public SoundsList[] sounds;
    
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
        // saving = DatabaseManager.element.LoadSaving();

        // MusicVolumeControl(saving.musicLevel / 3);
        // EffectsVolumeControl(saving.effectsLevel / 3);
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

    public void MusicVolumeControl(float volume)
    {
        // foreach (Sound s in sounds)
        // {
        //     if (s.music)
        //     {
        //         s.source.volume = volume;
        //     }
        // }
    }

    public void EffectsVolumeControl(float volume)
    {
        // foreach (Sound s in sounds)
        // {
        //     if (!s.music)
        //     {
        //         s.source.volume = volume;
        //     }
        // }
    }

    public void PlayRandomTrack()
    {
        int index = UnityEngine.Random.Range(1, 3);

        Play("menu-" + index);
    }
}
