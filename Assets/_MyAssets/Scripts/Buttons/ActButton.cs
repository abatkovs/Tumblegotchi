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
    [SerializeField] private Shop shop;
    [SerializeField] private SoundData selectionSound;
    [SerializeField] private Playground playground;
    [SerializeField] private Intro intro;
    
    private GameManager _gameManager;
    private SoundManager _soundManager;

    //TODO: Add small delay between button presses
    private void Start()
    {
        _soundManager = SoundManager.Instance;
        _gameManager = GameManager.Instance;
    }

    protected override void Action()
    {
        if (intro.IntroPlaying)
        {
            intro.FinishIntro();
            return;
        }
        if(GameManager.Instance.LockButtons) return;
        
        var selectedMenuOption = _gameManager.CurrentlySelectedMenuOption;
        switch (selectedMenuOption)
        {
            case MenuOptions.Garden:
                sceneSwitcher.SwitchScene();
                _soundManager.PlaySound(selectionSound);
                return;
            case MenuOptions.Feed:
                feeding.StartFeedingJelly();
                _soundManager.PlaySound(selectionSound);
                return;
            case MenuOptions.Pet:
                petting.StartPetting();
                _soundManager.PlaySound(selectionSound);
                return;
            case MenuOptions.Shop:
                sceneSwitcher.SwitchScene();
                _soundManager.PlaySound(selectionSound);
                return;
        }

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Garden)
        {
            //Try collect berries
            garden.GatherBerries();
            _soundManager.PlaySound(selectionSound);
        }

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Shop)
        {
            shop.TryBuyItem();
        }

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Playground)
        {
            playground.PlayWithJelly();
        }
    }
}
