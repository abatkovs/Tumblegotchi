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
    [SerializeField] private int loveIncreaseAmount = 5; 

    public PettingState CurrentPettingState { get; private set; } = PettingState.Idle;
    
    private JellyAnimator _animator;
    [SerializeField] private bool shouldStartPettingAgain;

    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<JellyAnimator>();
    }

    public void StartPetting()
    {
        if (CurrentPettingState == PettingState.Idle)
        {
            CurrentPettingState = PettingState.InitPetting;
            handItem.enabled = true;
            _gameManager.ToggleSelectionButton(true);
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

    public void ContinuePetting()
    {
        if (shouldStartPettingAgain)
        {
            shouldStartPettingAgain = false;
            return;
        }
        CurrentPettingState = PettingState.Petting;
        _animator.PlayPettingAnim();
    }

    /// <summary>
    /// Animation event
    /// </summary>
    public void FinishPetting()
    {
        CurrentPettingState = PettingState.Idle;
        _animator.PlayIdleAnim();
        _gameManager.ToggleSelectionButton(false);
        jellyStats.IncreaseLove(loveIncreaseAmount);
    }

    public void ResetPettingState()
    {
        CurrentPettingState = PettingState.Idle;
        _animator.PlayIdleAnim();
        handItem.enabled = false;
        _gameManager.ToggleSelectionButton(false);
    }
}
