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
            return;
        }
        if(item == playgroundLeft.CurrentItem) return;
        if (item == playgroundRight.CurrentItem) return; //TODO: Swap items.
        
        playgroundRight.SetPlaygroundItem(playgroundLeft.CurrentItem);
        playgroundLeft.SetPlaygroundItem(item);
    }
}
