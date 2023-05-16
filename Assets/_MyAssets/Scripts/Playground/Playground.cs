using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour
{
    // place on left 1st then cycle it to right
    [SerializeField] private PlaygroundSide playgroundLeft;
    [SerializeField] private PlaygroundSide playgroundRight;

    public void SetPlaygroundSprites(Sprite newSprite)
    {
        playgroundRight.SetPlaygroundSprite(playgroundLeft.CurrentSprite);
        playgroundLeft.SetPlaygroundSprite(newSprite);
    }
}
