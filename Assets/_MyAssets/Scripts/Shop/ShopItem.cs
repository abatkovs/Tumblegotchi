using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [field: SerializeField] public int ItemPrice { get; private set; }
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private Color selectedColor = Color.black;
    [SerializeField] private Color inactiveColor = Color.gray;

    private ValueToSprite _priceUI;

    private void Start()
    {
        if (_priceUI == null) _priceUI = GetComponent<ValueToSprite>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;
        _priceUI.SetSpriteNumbers(ItemPrice);
    }

    public void ToggleSelection(bool value)
    {
        if (value) spriteRenderer.color = selectedColor;
        if (!value) spriteRenderer.color = inactiveColor;
    }
}
