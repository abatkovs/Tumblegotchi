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

    public PettingState CurrentPettingState { get; private set; } = PettingState.Idle;
    
    private JellyAnimator _animator;
    private bool _shouldStartPettingAgain;

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
        }
        
        if (CurrentPettingState == PettingState.StartPetting)
        {
            _shouldStartPettingAgain = true;
        }
    }

    public void ContinuePetting()
    {
        if (_shouldStartPettingAgain)
        {
            _shouldStartPettingAgain = false;
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
    }

    public void ResetPettingState()
    {
        CurrentPettingState = PettingState.Idle;
        _animator.PlayIdleAnim();
        handItem.enabled = false;
        _gameManager.ToggleSelectionButton(false);
    }
}
