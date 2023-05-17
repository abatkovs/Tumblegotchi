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

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<JellyAnimator>();
        bgJelly.OnFinishMovingBack += BgJelly_OnOnFinishMovingBack;
    }

    private void BgJelly_OnOnFinishMovingBack()
    {
        _animator.PlayWalkOnScreenAnim();
    }

    public void ActivateBGJelly()
    {
        bgJelly.ActivateJelly();
    }

    public void MoveBGJellyToForeground()
    {
        bgJelly.MoveJellyBack();
    }

    public void FinishTransition()
    {
        _gameManager.ToggleSelectionButton(false);
        _gameManager.SwitchActiveScene(ActiveScene.Main);
        _animator.PlayIdleAnim();
    }
}
