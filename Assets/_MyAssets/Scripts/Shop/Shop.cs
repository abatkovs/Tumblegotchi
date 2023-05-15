using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private int selectedItem;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnSceneSwitch += GM_OnOnSceneSwitch;
    }

    private void OnDestroy()
    {
        _gameManager.OnSceneSwitch -= GM_OnOnSceneSwitch;
    }

    private void GM_OnOnSceneSwitch(ActiveScene obj)
    {
        if (obj == ActiveScene.Shop)
        {
            selectedItem = 0;
            shopItems[selectedItem].ToggleSelection(true);
        }
    }

    public void CycleSelection()
    {
        selectedItem++;
        var totalItems = shopItems.Count;
        if (selectedItem >= totalItems) selectedItem = 0;
        ClearSelection();
        shopItems[selectedItem].ToggleSelection(true);
    }

    private void ClearSelection()
    {
        foreach (var item in shopItems)
        {
            item.ToggleSelection(false);
        }
    }

    public void TryBuyItem()
    {
        var item = shopItems[selectedItem];
        if (item.ItemPrice > _gameManager.JellyDew)
        {
            Debug.Log($"Can't buy item: {item}");
        }
        else
        {
            Debug.Log("Buy item");
            item.BuyItem();
        }
        
    }
}
