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

    private void Start()
    {
        _soundManager = SoundManager.Instance;
    }

    protected override void Action()
    {
        if(GameManager.Instance.LockButtons) return;
        
        if (feeding.CurrentFeedingState == Feeding.FeedingState.StartFeeding)
        {
            feeding.ResetFeedingState();
            _soundManager.PlaySound(selectionSound);
            return;
        }

        if (petting.CurrentPettingState == Petting.PettingState.InitPetting)
        {
            petting.ResetPettingState();
            _soundManager.PlaySound(selectionSound);
            return;
        }
        
        sceneSwitcher.BackToMain();
        _soundManager.PlaySound(selectionSound);
    }
}
