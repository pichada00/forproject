using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSound, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSound, x => x.name == name);
        
        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = null;
            musicSource.clip = sound.clip;
            musicSource.Play();
            
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if(sound == null)
        {
            Debug.Log("Sound Not found");
        }
        else
        {
            //sfxSource.PlayOneShot(sound.clip);
            sfxSource.clip = sound.clip;
            sfxSource.Play();
            Invoke("stopsfxsource", 0.1f);
            
        }
    }

    public void stopsfxsource()
    {
        sfxSource.Stop();
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
