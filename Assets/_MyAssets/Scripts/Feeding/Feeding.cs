using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Feeding : MonoBehaviour
{
    [SerializeField] private int requiredBerriesForFeeding = 1;
    private GameManager _gameManager;
    private JellyStats _stats;
    private JellyAnimator _animator;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<JellyAnimator>();
        _stats = GetComponent<JellyStats>();
    }

    [ContextMenu("Feed Anim")]
    public void StartFeedingJelly()
    {
        if(_gameManager.Berries <= 0) return;
        _gameManager.AddBerries(-requiredBerriesForFeeding);
        _animator.PlayFeedAnim();
    }

    public void FinishFeedingAnimation()
    {
        Debug.Log("Finish feeding");
        _stats.FeedJelly();
        _animator.PlayIdleAnim();
    }
}
