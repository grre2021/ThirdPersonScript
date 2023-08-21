using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public AudioSource musicSource;

    public AudioClip backClip;
    
    
    
    
    protected override void Awake()
    {
        base.Awake();
        PlayBackgroundMusic(backClip);
        DontDestroyOnLoad(gameObject);
    }

    

    public void PlayBackgroundMusic(AudioClip audioClip)
    {
        
        musicSource.clip = audioClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopBackMusic()
    {
        musicSource.enabled = false;
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.Pause();
        audioSource.PlayOneShot(audioClip);
        
    }

    public void SettingBackgroundVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SettingVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
