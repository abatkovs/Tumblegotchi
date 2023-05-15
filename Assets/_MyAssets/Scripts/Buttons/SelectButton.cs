using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : BaseActionButton
{
    [SerializeField] protected Selection selection;
    [SerializeField] private Garden garden;
    [SerializeField] private Shop shop;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    protected override void Action()
    {
        if(_gameManager.LockButtons) return;
        if (_gameManager.CurrentlyActiveScene == ActiveScene.Garden)
        {
            CycleGardenSelection();
            return;
        }

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Shop)
        {
            CycleShopSelection();
            return;
        }
        selection.CycleSelection();
    }

    private void CycleGardenSelection()
    {
        garden.CycleSelection();
    }

    private void CycleShopSelection()
    {
        shop.CycleSelection();
    }
}
