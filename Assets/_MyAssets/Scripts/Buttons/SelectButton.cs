using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : BaseActionButton
{
    [SerializeField] protected Selection selection;
    [SerializeField] protected Garden garden;
    protected override void Action()
    {
        if(GameManager.Instance.LockButtons) return;
        if (GameManager.Instance.CurrentlyActiveScene == ActiveScene.Garden)
        {
            CycleGardenSelection();
            return;
        }
        selection.CycleSelection();
    }

    private void CycleGardenSelection()
    {
        garden.CycleSelection();
    }
}
