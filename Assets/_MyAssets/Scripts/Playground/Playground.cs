using System.Collections;
using System.Collections.Generic;
using _MyAssets.Scripts.Playground;
using UnityEngine;

public class Playground : MonoBehaviour
{
    // place on left 1st then cycle it to right
    [SerializeField] private PlaygroundSide playgroundLeft;
    [SerializeField] private PlaygroundSide playgroundRight;

    public void SetPlaygroundSprites(PlaygroundItem item)
    {
        if (playgroundLeft.CurrentItem == null)
        {
            playgroundLeft.SetPlaygroundItem(item);
            SaveManager.Instance.SaveLeftItem(playgroundLeft.CurrentItem.ItemID);
            return;
        }
        if(item == playgroundLeft.CurrentItem) return;
        if (item == playgroundRight.CurrentItem) return; //TODO: Swap items.
        
        playgroundRight.SetPlaygroundItem(playgroundLeft.CurrentItem);
        SaveManager.Instance.SaveRightItem(playgroundRight.CurrentItem.ItemID);
        playgroundLeft.SetPlaygroundItem(item);
        SaveManager.Instance.SaveLeftItem(playgroundLeft.CurrentItem.ItemID);
    }
    
    public void PlayWithJelly()
    {
        playgroundLeft.TryPlayWithJelly();
        playgroundRight.TryPlayWithJelly();
    }

    public void LoadItems(PlaygroundItem saveDataLeftItem, PlaygroundItem saveDataRightItem)
    {
        playgroundLeft.SetPlaygroundItem(saveDataLeftItem);
        playgroundRight.SetPlaygroundItem(saveDataRightItem);
    }
}
