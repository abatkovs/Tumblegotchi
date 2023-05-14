using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : BaseActionButton
{
    [SerializeField] private SceneSwitcher sceneSwitcher;
    protected override void Action()
    {
        sceneSwitcher.BackToMain();
    }
}
