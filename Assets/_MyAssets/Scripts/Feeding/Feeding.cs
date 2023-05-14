using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Feeding : MonoBehaviour
{
    public enum FeedingState
    {
        Idle,
        StartFeeding,
        Feeding,
    }
    
    [SerializeField] private int requiredBerriesForFeeding = 1;
    [SerializeField] private SpriteRenderer foodItem;
    [SerializeField] private Sprite food;
    private GameManager _gameManager;
    private JellyStats _stats;
    private JellyAnimator _animator;

    public FeedingState CurrentFeedingState { get; private set; }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<JellyAnimator>();
        _stats = GetComponent<JellyStats>();
    }

    [ContextMenu("Feed Anim")]
    public void StartFeedingJelly()
    {
        if(CurrentFeedingState == FeedingState.Feeding) return;
        if(_gameManager.Berries <= 0) return;
        //Show food before feeding
        if (CurrentFeedingState == FeedingState.Idle)
        {
            CurrentFeedingState = FeedingState.StartFeeding;
            foodItem.enabled = true;
            return;
        }
        
        _gameManager.AddBerries(-requiredBerriesForFeeding);
        _animator.PlayFeedAnim();
        foodItem.enabled = false;
        CurrentFeedingState = FeedingState.Feeding;
    }

    public void FinishFeedingAnimation()
    {
        Debug.Log("Finish feeding");
        _stats.FeedJelly();
        _animator.PlayIdleAnim();
        CurrentFeedingState = FeedingState.Idle;
    }

    public void ResetFeedingState()
    {
        _animator.PlayIdleAnim();
        CurrentFeedingState = FeedingState.Idle;
        foodItem.enabled = false;
    }
}
