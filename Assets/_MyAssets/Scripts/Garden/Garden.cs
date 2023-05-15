using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField] private List<BerryBush> berryBushes;
    [SerializeField] private int selectedBush;
    [SerializeField] private ShopItem saplingItem;

    private int _unlockedPlants = 1;
    private void Start()
    {
        saplingItem.OnSaplingBuy += AddSapling;
    }

    public void CycleSelection()
    {
        selectedBush++;
        var totalBushCount = berryBushes.Count;
        if (selectedBush >= totalBushCount) selectedBush = 0;
        ClearSelection();
        berryBushes[selectedBush].ToggleDrone(true);
    }

    private void ClearSelection()
    {
        foreach (var bush in berryBushes)
        {
            bush.ToggleDrone(false);
        }
    }

    public void GatherBerries()
    {
        berryBushes[selectedBush].GatherBerries();
    }

    public void AddSapling()
    {
        Debug.Log("Add sapling to garden");
        _unlockedPlants++;
        for (int i = 0; i < _unlockedPlants; i++)
        {
            if(berryBushes[i].hasSeed) continue;
            berryBushes[i].PlantSeed();
        }
    }
    
}
