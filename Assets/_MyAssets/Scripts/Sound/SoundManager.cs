using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource source;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(SoundData soundToPlay)
    {
        source.clip = soundToPlay.Sound.clip;
        source.loop = soundToPlay.Sound.loop;
        source.volume = soundToPlay.Volume;
        source.pitch = soundToPlay.Pitch;
        
        source.Play();
    }
    
}
