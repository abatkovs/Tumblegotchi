using System;
using System.Collections;
using System.Collections.Generic;
using _MyAssets.Scripts.Playground;
using UnityEngine;

public class PlaygroundSide : MonoBehaviour
{

    public PlaygroundItem CurrentItem { get; private set; }
    
    private SpriteRenderer _spriteRenderer;
    private PlaygroundAnimator _pgAnimator;

    private bool _isEmpty = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pgAnimator = GetComponent<PlaygroundAnimator>();
        _pgAnimator.RemoveController();
        _spriteRenderer.sprite = null;
    }

    public void SetPlaygroundItem(PlaygroundItem item)
    {
        _spriteRenderer.sprite = item.ItemSprite;
        CurrentItem = item;
    }
    
}
