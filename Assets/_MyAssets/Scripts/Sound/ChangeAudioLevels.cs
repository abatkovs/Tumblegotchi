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
    
    private Vector2 _startingPosition;
    private SoundManager _soundManager;

    private void Start()
    {
        _startingPosition = transform.position;
        _soundManager = SoundManager.Instance;
        button.OnButtonClicked += Button_OnOnButtonClicked;
        button.OnButtonReleased += Button_OnOnButtonReleased;
    }

    private void Button_OnOnButtonReleased()
    {
        ResetPosition();
    }

    private void Button_OnOnButtonClicked()
    {
        _soundManager.ChangeAudioLevel(amount);
        _soundManager.PlaySound(selectSound);
        audioLevels.StartCountdown();
    }
    
    private void ResetPosition()
    {
        transform.position = _startingPosition;
    }

}
