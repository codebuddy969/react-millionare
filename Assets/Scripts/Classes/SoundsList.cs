using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

[System.Serializable]
public class SoundsList
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    public bool music;

    [HideInInspector]
    public AudioSource source;
}