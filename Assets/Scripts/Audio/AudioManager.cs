using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
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
