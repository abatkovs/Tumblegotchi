using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private int selectedItem;

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
}
