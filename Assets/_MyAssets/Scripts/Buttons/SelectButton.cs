using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : BaseActionButton
{
    [SerializeField] protected Selection selection;
    [SerializeField] private Garden garden;
    [SerializeField] private Shop shop;
    [SerializeField] private SoundData selectionSound;
    
    private GameManager _gameManager;
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        _gameManager = GameManager.Instance;
    }

    protected override void Action()
    {
        if(_gameManager.LockSelectionButton) return;
        if(_gameManager.LockButtons) return;
        if (_gameManager.CurrentlyActiveScene == ActiveScene.Garden)
        {
            CycleGardenSelection();
            _soundManager.PlaySound(selectionSound);
            return;
        }

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Shop)
        {
            CycleShopSelection();
            return;
        }
        selection.CycleSelection();
        _soundManager.PlaySound(selectionSound);
    }

    private void CycleGardenSelection()
    {
        garden.CycleSelection();
        _soundManager.PlaySound(selectionSound);
    }

    private void CycleShopSelection()
    {
        shop.CycleSelection();
        _soundManager.PlaySound(selectionSound);
    }
}
