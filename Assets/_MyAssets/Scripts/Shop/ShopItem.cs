using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopItem : MonoBehaviour
{
    [field: SerializeField] public int ItemPrice { get; private set; }
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private Color selectedColor = Color.black;
    [SerializeField] private Color inactiveColor = Color.gray;
    [Space]
    [SerializeField] private bool isItemStackable;
    [SerializeField] private int maxItemStackSize = 1;
    
    public bool IsItemBought;

    private int _currentStackSize = 1;
    private int _currentItemPrice;
    private ValueToSprite _priceUI;
    private GameManager _gameManager;

    public event Action OnSaplingBuy;
    
    private void Start()
    {
        if(_gameManager == null) _gameManager = GameManager.Instance;
        if (_priceUI == null) _priceUI = GetComponent<ValueToSprite>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;
        _priceUI.SetSpriteNumbers(ItemPrice);
        ToggleSelection(false);
        _currentItemPrice = ItemPrice;
    }

    public void ToggleSelection(bool value)
    {
        if (value) spriteRenderer.color = selectedColor;
        if (!value) spriteRenderer.color = inactiveColor;
    }

    //TODO: Fix this mess idk...
    public void BuyItem()
    {
        if (_currentStackSize > maxItemStackSize)
        {
            isItemStackable = false;
            _currentItemPrice = 0;
            _priceUI.SetSpriteNumbers(_currentItemPrice);
        }
        if (isItemStackable)
        {
            _currentItemPrice = ItemPrice * _currentStackSize;
            _currentStackSize++;
            _gameManager.AddJellyDew(-_currentItemPrice);
            _priceUI.SetSpriteNumbers(ItemPrice * _currentStackSize);
            OnSaplingBuy?.Invoke();
            if (_currentStackSize == 6)
            {
                _priceUI.SetSpriteNumbers(0);
            }
            return;
        }
        
        _gameManager.AddJellyDew(-_currentItemPrice);
        
        if (!isItemStackable)
        {
            IsItemBought = true;
            _currentItemPrice = 0;
            _priceUI.SetSpriteNumbers(_currentItemPrice);
        }
    }
}
