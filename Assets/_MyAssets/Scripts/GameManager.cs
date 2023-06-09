using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField] public Playground Playground { get; private set; }
    [field: SerializeField] public bool LockButtons { get; set; }
    [field: SerializeField] public bool LockSelectionButton { get; private set; }
    [field: SerializeField] public MenuOptions CurrentlySelectedMenuOption { get; private set; } = MenuOptions.Garden;
    [field: SerializeField] public ActiveScene CurrentlyActiveScene { get; private set; } = ActiveScene.Main;

    [field: FormerlySerializedAs("berries")] [field: SerializeField] public int Berries { get; private set; }
    [field: FormerlySerializedAs("currency")] [field: SerializeField] public int JellyDew { get; private set; }
    [SerializeField] private ValueToSprite berriesUI;
    [FormerlySerializedAs("currencyUI")] [SerializeField] private ValueToSprite jellyDewUI;

    public event Action<ActiveScene> OnSceneSwitch;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        berriesUI.SetSpriteNumbers(Berries);
        jellyDewUI.SetSpriteNumbers(JellyDew);
    }

    public void SwitchActiveMenuSelection(MenuOptions selection)
    {
        CurrentlySelectedMenuOption = selection;
    }

    public void SwitchActiveScene(ActiveScene scene)
    {
        OnSceneSwitch?.Invoke(scene);
        CurrentlyActiveScene = scene;
        if(scene != ActiveScene.Main) CurrentlySelectedMenuOption = MenuOptions.None;
    }

    public void AddBerries(int berryYield)
    {
        var min = 0;
        var max = 99;
        Berries = Mathf.Clamp(Berries + berryYield, min, max);
        berriesUI.SetSpriteNumbers(Berries);
        SaveManager.Instance.SaveBerries(Berries);
    }

    public void AddJellyDew(int jellyDewYield)
    {
        var min = 0;
        var max = 99;
        JellyDew = Mathf.Clamp(JellyDew + jellyDewYield, min, max);
        jellyDewUI.SetSpriteNumbers(JellyDew);
        SaveManager.Instance.SaveJellyDew(JellyDew);
    }

    public void SetBerriesAndJellyDew(int berries, int jellyDew)
    {
        JellyDew = jellyDew;
        Berries = berries;
    }


    public void UpdateSelection()
    {
        SwitchActiveMenuSelection(CurrentlySelectedMenuOption);
    }
    /// <summary>
    /// Enable or dissable selection button
    /// </summary>
    /// <param name="value"></param>
    public void ToggleSelectionButton(bool value)
    {
        LockSelectionButton = value;
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
    Playground,
}