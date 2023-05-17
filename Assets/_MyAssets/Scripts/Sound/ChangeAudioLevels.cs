using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudioLevels : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int amount = 1;
    [SerializeField] private SoundData selectSound;
    [SerializeField] private AudioLevels audioLevels;
    
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        button.OnButtonClicked += Button_OnOnButtonClicked;
    }

    private void Button_OnOnButtonClicked()
    {
        _soundManager.ChangeAudioLevel(amount);
        _soundManager.PlaySound(selectSound);
        audioLevels.StartCountdown();
    }


}
