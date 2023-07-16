using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Jelly : MonoBehaviour
{
    [SerializeField] private JellyBG bgJelly;

    private GameManager _gameManager;
    private JellyAnimator _animator;

    public event Action OnFinishComingBack;
    public event Action OnStartWalkingToBG;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<JellyAnimator>();
        bgJelly.OnFinishMovingBack += BgJelly_OnOnFinishMovingBack;
    }

    private void BgJelly_OnOnFinishMovingBack()
    {
        OnFinishComingBack?.Invoke();
        _animator.PlayWalkOnScreenAnim();
    }

    private void StartWalkingToBG()
    {
        OnStartWalkingToBG?.Invoke();
    }

    public void ActivateBGJelly()
    {
        bgJelly.ActivateBGJelly();
    }

    public void MoveBGJellyToForeground()
    {
        bgJelly.JellyWavedAt();
    }

    public void FinishTransition()
    {
        _gameManager.ToggleSelectionButton(false);
        _gameManager.SwitchActiveScene(ActiveScene.Main);
        _animator.PlayIdleAnim();
    }
}
