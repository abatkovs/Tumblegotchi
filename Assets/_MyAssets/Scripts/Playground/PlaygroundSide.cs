using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundSide : MonoBehaviour
{

    public Sprite CurrentSprite { get; private set; }
    
    private SpriteRenderer _spriteRenderer;
    private PlaygroundAnimator _animator;

    private bool _isEmpty = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<PlaygroundAnimator>();
        _spriteRenderer.sprite = null;
    }

    public void SetPlaygroundSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        CurrentSprite = sprite;
    }
    
}
