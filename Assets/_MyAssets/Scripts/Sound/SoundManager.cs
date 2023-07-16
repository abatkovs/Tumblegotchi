using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Audio;
using FMODUnity;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private List<SoundLevelOptions> mixerSoundLevels;
    [SerializeField] private StudioEventEmitter source;
    [SerializeField] private StudioEventEmitter source2;
    
    [SerializeField] private AudioLevels audioLevels;

    [SerializeField] private string audioBusPath = "bus:/SFX";
    private Bus _fmodAudioBus;
    private int _selectedSoundLevel;
    private bool _loaded;
    
    private void Awake()
    {
        Instance = this;
        _fmodAudioBus = RuntimeManager.GetBus(audioBusPath);
    }

    private void Start()
    {
        if(_loaded) return;
        ChangeAudioLevel(5);
    }

    private void PlayAudioFromSource(StudioEventEmitter eventEmiter, SoundData soundToPlay)
    {
        RuntimeManager.PlayOneShot(soundToPlay.SoundEvent);
    }

    public void PlaySound(SoundData soundToPlay)
    {
        PlayAudioFromSource(source, soundToPlay);
    }

    public void PlaySound2(SoundData soundToPlay)
    {
        RuntimeManager.PlayOneShot(soundToPlay.SoundEvent);
    }

    public void ChangeAudioLevel(int amount)
    {
        _selectedSoundLevel = Mathf.Clamp(_selectedSoundLevel + amount, 0, mixerSoundLevels.Count-1);
        audioLevels.ChangeSoundLevel(amount);
        _fmodAudioBus.setVolume(mixerSoundLevels[_selectedSoundLevel].soundLevel);
    }

    public void Load()
    {
        _loaded = true;
    }
}

[Serializable]
public class SoundLevelOptions
{
    public float soundLevel;
}
