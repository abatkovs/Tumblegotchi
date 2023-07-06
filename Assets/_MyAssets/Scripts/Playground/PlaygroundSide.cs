using System;
using System.Collections;
using System.Collections.Generic;
using _MyAssets.Scripts.Playground;
using UnityEngine;

public class PlaygroundSide : MonoBehaviour
{

    [field: SerializeField] public PlaygroundItem CurrentItem { get; private set; }
    
    private SpriteRenderer _spriteRenderer;
    private PlaygroundAnimator _pgAnimator;

    private bool _isEmpty = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pgAnimator = GetComponent<PlaygroundAnimator>();
        _spriteRenderer.sprite = null;
    }


    public void SetPlaygroundItem(PlaygroundItem item)
    {
        _spriteRenderer.sprite = item.ItemSprite;
        CurrentItem = item;
        item.SetPlaygroundAnimator(_pgAnimator);
        _pgAnimator.SetSprite(item.ItemSprite);
        _pgAnimator.SetPlaygroundItem(CurrentItem);
    }

    public void StartAnimation(JellyBG jellyBg)
    {
        _pgAnimator.JellyBG = jellyBg;
        _pgAnimator.ResetNextAnim();
        _pgAnimator.SetJellyBoredTimers(CurrentItem.TimeUntilJellyGetsBored);
        _pgAnimator.PlayEnterAnimation(CurrentItem.EnterAnimationString);
    }

    public void TryPlayWithJelly()
    {
        _pgAnimator.PlayWithJelly();
    }
}
