using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : BaseActionButton
{
    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private Feeding feeding;
    [SerializeField] private Petting petting;
    protected override void Action()
    {
        if(GameManager.Instance.LockButtons) return;
        
        if (feeding.CurrentFeedingState == Feeding.FeedingState.StartFeeding)
        {
            feeding.ResetFeedingState();
            return;
        }

        if (petting.CurrentPettingState == Petting.PettingState.InitPetting)
        {
            petting.ResetPettingState();
            return;
        }
        
        sceneSwitcher.BackToMain();
    }
}
