using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField] private List<BerryBush> berryBushes;

    [SerializeField] private int selectedBush;
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
    
}
