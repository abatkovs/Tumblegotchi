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
        if(GameManager.Instance.LockButtons) return;
        
        var selectedMenuOption = _gameManager.CurrentlySelectedMenuOption;
        switch (selectedMenuOption)
        {
            case MenuOptions.Garden:
                sceneSwitcher.SwitchScene();
                break;
            case MenuOptions.Feed:
                feeding.StartFeedingJelly();
                break;
            case MenuOptions.Pet:
                petting.StartPetting();
                break;
            case MenuOptions.Shop:
                sceneSwitcher.SwitchScene();
                break;
        }

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Garden)
        {
            //Try collect berries
            garden.GatherBerries();
        }
    }
}
