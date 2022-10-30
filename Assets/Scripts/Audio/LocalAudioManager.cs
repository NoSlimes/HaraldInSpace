using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class LocalAudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.priority = s.priority;
            s.source.spatialBlend = s.spatialBlend;
            s.source.rolloffMode = s.rolloffMode;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: \"" + name + "\" was not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop()
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "was stopped!");
            return;
        }
        s.source.Stop();
    }

    //Place sounds that you want to play at the start of the game in the Start() function
    private void Start()
    {
            //Play("");
    }

    private void Update()
    {
          
    }
}
