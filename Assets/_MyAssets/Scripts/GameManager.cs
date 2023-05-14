using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField] public MenuOptions CurrentlySelectedMenuOption { get; private set; } = MenuOptions.Garden;
    [field: SerializeField] public ActiveScene CurrentlyActiveScene { get; private set; } = ActiveScene.Main;

    [SerializeField] private int berries;
    [SerializeField] private int currency;
    [SerializeField] private ValueToSprite berriesUI;
    [SerializeField] private ValueToSprite currencyUI;

    public event Action<ActiveScene> OnSceneSwitch;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        berriesUI.SetSpriteNumbers(berries);
        currencyUI.SetSpriteNumbers(currency);
    }

    public void SwitchActiveMenuSelection(MenuOptions selection)
    {
        CurrentlySelectedMenuOption = selection;
    }

    public void SwitchActiveScene(ActiveScene scene)
    {
        OnSceneSwitch?.Invoke(scene);
        CurrentlyActiveScene = scene;
        CurrentlySelectedMenuOption = MenuOptions.None;
    }
}

public enum MenuOptions
{
    None,
    Garden,
    Feed,
    Pet,
    Shop,
}

public enum ActiveScene
{
    Main,
    Garden,
    Shop,
}