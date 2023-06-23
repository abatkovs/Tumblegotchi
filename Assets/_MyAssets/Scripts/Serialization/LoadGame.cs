using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private GameObject selectionScene;
    [SerializeField] private GameObject gameScene;
    [SerializeField] private GameObject tamagotchiScene;
    [SerializeField] private GameObject introScene;
    
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
        GameManager.Instance.AddBerries(SaveManager.Instance.SaveData.Berries);
        GameManager.Instance.AddJellyDew(SaveManager.Instance.SaveData.JellyDew);
    }
}
