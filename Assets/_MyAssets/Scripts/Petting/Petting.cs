using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petting : MonoBehaviour
{
    public enum PettingState
    {
        Idle,
        InitPetting,
        StartPetting,
        Petting,
    }
    
    [SerializeField] private SpriteRenderer handItem;
    [SerializeField] private JellyStats jellyStats;
    [SerializeField] private int moodIncreaseAmount = 5;
    [SerializeField] private int moodDecreaseAmount = 5;
    [SerializeField] private SoundData cantPetNowSound;
    [SerializeField] private bool shouldStartPettingAgain;
    
    public PettingState CurrentPettingState { get; private set; } = PettingState.Idle;
    
    private GameManager _gameManager;
    private JellyAnimator _animator;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<JellyAnimator>();
    }

    public void StartPetting()
    {
        if (jellyStats.GetJellyAge() == JellyStats.JellyAge.Egg)
        {
            SoundManager.Instance.PlaySound(cantPetNowSound);
            return;
        }

        if (jellyStats.CurrentJellyState == JellyStats.JellyState.Sleeping)
        {
            SoundManager.Instance.PlaySound(cantPetNowSound);
            jellyStats.ChangeMoodLevel(moodDecreaseAmount);
            jellyStats.WakeUpJelly();
            PetJelly();
            return;
        }
        if (CurrentPettingState == PettingState.Idle)
        {
            PetJelly();
            return;
        }

        handItem.enabled = false;

        if (CurrentPettingState == PettingState.InitPetting)
        {
            CurrentPettingState = PettingState.StartPetting;
            _animator.PlayStartPettingAnim();
            return;
        }
        
        if (CurrentPettingState == PettingState.StartPetting)
        {
            shouldStartPettingAgain = true;
        }
    }

    private void PetJelly()
    {
        CurrentPettingState = PettingState.InitPetting;
        handItem.enabled = true;
        _gameManager.ToggleSelectionButton(true);
    }

    public void ContinuePetting()
    {
        if (shouldStartPettingAgain)
        {
            shouldStartPettingAgain = false;
            return;
        }
        CurrentPettingState = PettingState.Petting;
        _animator.PlayPettingAnim(jellyStats.GetLoveLevel());
    }

    /// <summary>
    /// Animation event
    /// </summary>
    public void FinishPetting()
    {
        CurrentPettingState = PettingState.Idle;
        _animator.PlayIdleAnim();
        _gameManager.ToggleSelectionButton(false);
        //jellyStats.IncreaseLove(loveIncreaseAmount);
        jellyStats.ChangeMoodLevel(moodIncreaseAmount);
    }

    public void ResetPettingState()
    {
        CurrentPettingState = PettingState.Idle;
        _animator.PlayIdleAnim();
        handItem.enabled = false;
        _gameManager.ToggleSelectionButton(false);
    }
}
