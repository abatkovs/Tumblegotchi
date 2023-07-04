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
    [SerializeField] private Jelly jelly;
    [SerializeField] private JellyStats jellyStats;
    [Space] 
    [SerializeField] private SoundData cantMoveNowSound;
    
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



        if (_gameManager.CurrentlyActiveScene == ActiveScene.Playground)
        {
            sceneSwitcher.PlaygroundToMain();
            jelly.MoveBGJellyToForeground();
            
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
        
        if (_gameManager.CurrentlyActiveScene == ActiveScene.Main)
        {
            if (jellyStats.GetJellyAge() == JellyStats.JellyAge.Egg)
            {
                SoundManager.Instance.PlaySound(cantMoveNowSound);
                return;
            }
            sceneSwitcher.SwitchToPlayground();
            _gameManager.ToggleSelectionButton(true);
            return;
        }
        
        sceneSwitcher.BackToMain();
        petting.ResetPettingState();
        _soundManager.PlaySound(selectionSound);
    }
}
