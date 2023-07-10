using System;
using System.Collections;
using System.Collections.Generic;
using _MyAssets.Scripts.Jelly;
using _MyAssets.Scripts.Playground;
using UnityEditor;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private GameObject selectionScene;
    [SerializeField] private GameObject gameScene;
    [SerializeField] private GameObject tamagotchiScene;
    [SerializeField] private GameObject introScene;
    [Space(20)] 
    [SerializeField] private SpriteRenderer shellSprite;
    [SerializeField] private SpriteRenderer screenSprite;
    [SerializeField] private JellyStats jelly;
    [Space(20)] 
    [SerializeField] private Garden garden;
    [Space] 
    [SerializeField] private Shop shop;
    [Space]
    [SerializeField] private JellyStats jellyStats;
    [Space]
    [SerializeField] private Playground playground;
    [Space] 
    [SerializeField] private AudioLevels audioLevels;
    
    private void Start()
    {
        SaveManager.Instance.LoadGame();
        if (SaveManager.Instance.SaveFileExists)
        {
            gameScene.SetActive(true);
            tamagotchiScene.SetActive(true);
            introScene.SetActive(false);
            SetLoadData();
        }
        if(!SaveManager.Instance.SaveFileExists) selectionScene.SetActive(true);
    }

    private void SetLoadData()
    {
        GameManager.Instance.SetBerriesAndJellyDew(SaveManager.Instance.SaveData.Berries, SaveManager.Instance.SaveData.JellyDew);
        LoadShellData();
        LoadEvolutionData();
        LoadGardenData();
        LoadShopItems();
        LoadJellyStats();
        LoadPlaygroundItems();
        LoadAudioLevels();
    }

    /// <summary>
    /// Loads look of outer shell
    /// </summary>
    private void LoadShellData()
    {
        var shells = Resources.LoadAll<ShellData>("Shells");
        var targetShellID = SaveManager.Instance.SaveData.SelectedShell;
        foreach (var shell in shells)
        {
            if (shell.ID == targetShellID)
            {
                shellSprite.sprite = shell.ShellSprite;
                screenSprite.color = shell.ScreenColor;
                break;
            }
        }
    }

    private void LoadEvolutionData()
    {
        var evolutions = Resources.LoadAll<JellyEvolutionData>("Evolutions");
        var targetEvolution = SaveManager.Instance.SaveData.SelectedEvolution;

        foreach (var evo in evolutions)
        {
            if (evo.ID == targetEvolution)
            {
                jelly.ChangeEvolutionData(evo);
            }
        }
    }

    private void LoadGardenData()
    {
        garden.LoadSaplings(SaveManager.Instance.SaveData.UnlockedPlants);
    }

    private void LoadShopItems()
    {
        shop.LoadItems(SaveManager.Instance.SaveData.ShopItems);
    }

    private void LoadPlaygroundItems()
    {
        var playgroundItems = Resources.LoadAll<PlaygroundItem>("Playground");
        var leftSideItemID = SaveManager.Instance.SaveData.LeftItemID;
        var rightSideItemID = SaveManager.Instance.SaveData.RightItemID;
        PlaygroundItem leftSideItem = null;
        PlaygroundItem rightSideItem = null;
        foreach (var playgroundItem in playgroundItems)
        {
            if (leftSideItemID == playgroundItem.ItemID)
            {
                leftSideItem = playgroundItem;
            }

            if (rightSideItemID == playgroundItem.ItemID)
            {
                rightSideItem = playgroundItem;
            }
        }
        playground.LoadItems(leftSideItem, rightSideItem);
    }

    private void LoadJellyStats()
    {
        jellyStats.LoadStats();
    }
    
    
    private void LoadAudioLevels()
    {
        audioLevels.LoadAudioLevels();
    }
}
