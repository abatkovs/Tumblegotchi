using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActButton : BaseActionButton
{
    [SerializeField] private SceneSwitcher sceneSwitcher;
    protected override void Action()
    {
        var selectedMenuOption = GameManager.Instance.CurrentlySelectedMenuOption;
        if(selectedMenuOption == MenuOptions.Garden) sceneSwitcher.SwitchScene();
    }
}
