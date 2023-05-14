using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petting : MonoBehaviour
{
    public enum PettingState
    {
        Idle,
        StartPetting,
        Petting,
        FinishPetting,
    }
    
    [SerializeField] private SpriteRenderer handItem;

    public PettingState CurrentPettingState { get; private set; } = PettingState.Idle;
    
    private JellyAnimator _animator;
    private bool _shouldStartPettingAgain;

    private void Start()
    {
        _animator = GetComponent<JellyAnimator>();
    }

    public void StartPetting()
    {
        if (CurrentPettingState == PettingState.StartPetting)
        {
            _shouldStartPettingAgain = true;
        }
        if (CurrentPettingState == PettingState.Idle)
        {
            CurrentPettingState = PettingState.StartPetting;
            _animator.PlayStartPettingAnim();
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

    public void FinishPetting()
    {
        CurrentPettingState = PettingState.Idle;
        _animator.PlayIdleAnim();
    }
}
