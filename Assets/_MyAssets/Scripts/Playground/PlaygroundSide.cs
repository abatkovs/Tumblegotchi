using System;
using System.Collections;
using System.Collections.Generic;
using _MyAssets.Scripts.Playground;
using UnityEngine;

public class PlaygroundSide : MonoBehaviour
{

    [field: SerializeField] public PlaygroundItem CurrentItem { get; private set; }
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlaygroundAnimator pgAnimator;

    private bool _isEmpty = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pgAnimator = GetComponent<PlaygroundAnimator>();
        spriteRenderer.sprite = null;
    }


    public void SetPlaygroundItem(PlaygroundItem item)
    {
        if(item is null) return;
        spriteRenderer.sprite = item.ItemSprite;
        CurrentItem = item;
        item.SetPlaygroundAnimator(pgAnimator);
        pgAnimator.SetSprite(item.ItemSprite);
        pgAnimator.SetPlaygroundItem(CurrentItem);
    }

    public void StartAnimation(JellyBG jellyBg)
    {
        pgAnimator.JellyBG = jellyBg;
        pgAnimator.ResetNextAnim();
        pgAnimator.SetJellyBoredTimers(CurrentItem.TimeUntilJellyGetsBored);
        pgAnimator.PlayEnterAnimation(CurrentItem.EnterAnimationString);
    }

    public void TryPlayWithJelly()
    {
        pgAnimator.PlayWithJelly();
    }
}
