using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActButton : BaseActionButton
{
    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private Garden garden;
    [SerializeField] private Feeding feeding;
    [SerializeField] private Petting petting;

    private GameManager _gameManager;

    //TODO: Add small delay between button presses
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    protected override void Action()
    {
        var selectedMenuOption = _gameManager.CurrentlySelectedMenuOption;
        if(selectedMenuOption == MenuOptions.Garden) sceneSwitcher.SwitchScene();
        if(selectedMenuOption == MenuOptions.Feed) feeding.StartFeedingJelly();
        if(selectedMenuOption == MenuOptions.Pet) petting.StartPetting();
        if (_gameManager.CurrentlyActiveScene == ActiveScene.Garden)
        {
            //Try collect berries
            garden.GatherBerries();
        }
    }
}
