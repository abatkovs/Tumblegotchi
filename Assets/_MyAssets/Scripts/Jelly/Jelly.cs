using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] private JellyBG bgJelly;

    private GameManager _gameManager;
    private JellyAnimator _animator;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<JellyAnimator>();
    }

    public void ActivateBGJelly()
    {
        bgJelly.ActivateJelly();
    }

    public void DeactivateBGJelly()
    {
        bgJelly.DeactivateJelly();
    }

    public void FinishTransition()
    {
        _gameManager.ToggleSelectionButton(false);
        _gameManager.SwitchActiveScene(ActiveScene.Main);
        _animator.PlayIdleAnim();
    }
}
