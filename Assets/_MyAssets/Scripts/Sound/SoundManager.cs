using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource source2;

    [SerializeField] private AudioLevels audioLevels;

    private void Awake()
    {
        Instance = this;
    }
    
    private void PlayAudioFromSource(AudioSource sourceToPlayFrom, SoundData soundToPlay)
    {
        sourceToPlayFrom.clip = soundToPlay.Sound.clip;
        sourceToPlayFrom.loop = soundToPlay.Sound.loop;
        sourceToPlayFrom.volume = soundToPlay.Volume;
        sourceToPlayFrom.pitch = soundToPlay.Pitch;
        
        sourceToPlayFrom.Play();
    }

    public void PlaySound(SoundData soundToPlay)
    {
        PlayAudioFromSource(source, soundToPlay);
    }

    public void PlaySound2(SoundData soundToPlay)
    {
        PlayAudioFromSource(source2, soundToPlay);
    }

    public void ChangeAudioLevel(int amount)
    {
        audioLevels.ChangeSoundLevel(amount);
    }
    
}
