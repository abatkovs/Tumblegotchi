using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActButton : BaseActionButton
{
    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private Garden garden;
    protected override void Action()
    {
        var selectedMenuOption = GameManager.Instance.CurrentlySelectedMenuOption;
        if(selectedMenuOption == MenuOptions.Garden) sceneSwitcher.SwitchScene();
        if (GameManager.Instance.CurrentlyActiveScene == ActiveScene.Garden)
        {
            //Try collect berries
            garden.GatherBerries();
        }
    }
}
