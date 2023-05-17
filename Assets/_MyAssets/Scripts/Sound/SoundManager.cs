using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private List<SoundLevelOptions> mixerSoundLevels;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource source2;

    [SerializeField] private AudioLevels audioLevels;

    private int _selectedSoundLevel = 5;
    
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
        _selectedSoundLevel = Mathf.Clamp(_selectedSoundLevel + amount, 0, mixerSoundLevels.Count-1);
        audioLevels.ChangeSoundLevel(amount);
        mixer.SetFloat("Volume", mixerSoundLevels[_selectedSoundLevel].soundLevel);
    }
    
}

[Serializable]
public class SoundLevelOptions
{
    public float soundLevel;
}
