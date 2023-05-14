using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : BaseActionButton
{
    [SerializeField] protected Selection selection;
    protected override void Action()
    {
        selection.CycleSelection();
    }
}
