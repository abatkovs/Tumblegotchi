using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private int berries;
    [SerializeField] private int currency;
    [SerializeField] private ValueToSprite berriesUI;
    [SerializeField] private ValueToSprite currencyUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        berriesUI.SetSpriteNumbers(berries);
        currencyUI.SetSpriteNumbers(currency);
    }
}
