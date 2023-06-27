using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private int selectedItem;
    [Space]
    [SerializeField] private SoundData cantBuySound;

    private GameManager _gameManager;
    private SoundManager _soundManager;

    public event Action OnItemBuy;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
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
        if (item.CurrentItemPrice > _gameManager.JellyDew)
        {
            Debug.Log($"Can't buy item not enough money: {item}");
            _soundManager.PlaySound(cantBuySound);
            SaveItems();
        }
        else
        {
            if (item.IsItemBought)
            {
                OnItemBuy?.Invoke();
                item.UpdatePlaygroundItems();
                SaveItems();
                return;
            }
            OnItemBuy?.Invoke();
            item.TryToBuyItem();
            SaveItems();
        }
        
    }

    private void SaveItems()
    {
        List<int> boughtItems = new List<int>();
        foreach (var item in shopItems)
        {
            if (!item.isItemStackable)
            {
                if(item.IsItemBought) boughtItems.Add(1);
                if(!item.IsItemBought) boughtItems.Add(0);
            }

            if (item.isItemStackable)
            {
                if(item.IsItemBought) boughtItems.Add(-1);
                if(!item.IsItemBought) boughtItems.Add(item.CurrentStackSize);
            }

        }
        SaveManager.Instance.SaveShopData(boughtItems);
    }

    public void LoadItems(List<int> saveDataShopItems)
    {
        if(saveDataShopItems == null) return;
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (saveDataShopItems[i] == 1)
            {
                shopItems[i].LoadItemStatus();
            }
            if(shopItems[i].isItemStackable) 
            {
                shopItems[i].LoadItemStatus(saveDataShopItems[i], true);
            }
        }
    }
}
