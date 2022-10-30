using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixer;

    [Range(0, 256)]
    public int priority;
    [Range(0f, 5f)]
    public float volume = 1;
    [Range(-3f, 3f)]
    public float pitch = 1;
    [Range(-1, 1)]
    public float stereoPan = 1;
    [Range(0, 1)]
    public float spatialBlend;

    public bool loop;
    public bool playOnAwake;

    public AudioRolloffMode rolloffMode;
    public float minDistance = 1;
    public float maxDistance = 500;

    [HideInInspector]
    public AudioSource source;
}
