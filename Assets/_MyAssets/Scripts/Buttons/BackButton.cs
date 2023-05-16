using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : BaseActionButton
{
    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private Feeding feeding;
    [SerializeField] private Petting petting;
    [SerializeField] private SoundData selectionSound;

    private SoundManager _soundManager;
    private GameManager _gameManager;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        _gameManager = GameManager.Instance;
    }

    protected override void Action()
    {
        if(_gameManager.LockButtons) return;

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Main)
        {
            sceneSwitcher.SwitchToPlayground();
            _gameManager.ToggleSelectionButton(true);
            return;
        }

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Playground)
        {
            sceneSwitcher.PlaygroundToMain();
            return;
        }
        
        if (feeding.CurrentFeedingState == Feeding.FeedingState.StartFeeding)
        {
            feeding.ResetFeedingState();
            _soundManager.PlaySound(selectionSound);
            _gameManager.ToggleSelectionButton(false);
            return;
        }

        if (petting.CurrentPettingState == Petting.PettingState.InitPetting)
        {
            petting.ResetPettingState();
            _soundManager.PlaySound(selectionSound);
            _gameManager.ToggleSelectionButton(false);
            return;
        }
        
        sceneSwitcher.BackToMain();
        _soundManager.PlaySound(selectionSound);
    }
}
