using System;
using System.Collections;
using System.Collections.Generic;
using _MyAssets.Scripts.Playground;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopItem : MonoBehaviour
{
    [field: SerializeField] public int ItemPrice { get; private set; }
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<SpriteRenderer> currencySprites;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private Color selectedColor = Color.black;
    [SerializeField] private Color inactiveColor = Color.gray;
    [Space]
    [SerializeField] public bool isItemStackable;
    [SerializeField] private int maxItemStackSize = 1;
    [SerializeField] private bool isPlaygroundItem = true;
    [SerializeField] private PlaygroundItem playgroundItem;
    
    public bool IsItemBought;

    [field:Space(25)]
    [field: SerializeField] public int CurrentStackSize { get; private set; } = 1;
    [field: SerializeField] public int CurrentItemPrice { get; private set; }
    
    [SerializeField] private ValueToSprite _priceUI;
    private GameManager _gameManager;

    public event Action OnSaplingBuy;
    
    private void Start()
    {
        if(_gameManager == null) _gameManager = GameManager.Instance;
        if (_priceUI == null) _priceUI = GetComponent<ValueToSprite>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;
        if(CurrentItemPrice != 0) _priceUI.SetSpriteNumbers(CurrentItemPrice);
        ToggleSelection(false);
        if (CurrentItemPrice == 0)
        {
            CurrentItemPrice = ItemPrice;
            _priceUI.SetSpriteNumbers(CurrentItemPrice);
        }
    }
    
    public void ToggleSelection(bool value)
    {
        if (value)
        {
            ChangeSpriteColors(selectedColor);
        }

        if (!value)
        {
            ChangeSpriteColors(inactiveColor);
        }
    }

    private void ChangeSpriteColors(Color color)
    {
        spriteRenderer.color = color;
        foreach (var cur in currencySprites)
        {
            cur.color = color;
        }
    }

    //TODO: Fix this mess idk...
    public void TryToBuyItem()
    {
        if (isPlaygroundItem)
        {
            _gameManager.Playground.SetPlaygroundSprites(playgroundItem);
        }
        if (CurrentStackSize > maxItemStackSize)
        {
            isItemStackable = false;
            CurrentItemPrice = 0;
            _priceUI.SetSpriteNumbers(CurrentItemPrice);
        }
        if (isItemStackable)
        {
            CurrentItemPrice = ItemPrice * CurrentStackSize;
            CurrentStackSize++;
            _gameManager.AddJellyDew(-CurrentItemPrice);
            _priceUI.SetSpriteNumbers(ItemPrice * CurrentStackSize);
            CurrentItemPrice = ItemPrice * CurrentStackSize;
            OnSaplingBuy?.Invoke();
            if (CurrentStackSize == 6)
            {
                _priceUI.SetSpriteNumbers(0);
            }
            return;
        }
        
        _gameManager.AddJellyDew(-CurrentItemPrice);
        
        if (!isItemStackable)
        {
            IsItemBought = true;
            CurrentItemPrice = 0;
            _priceUI.SetSpriteNumbers(CurrentItemPrice);
        }
    }

    public void UpdatePlaygroundItems()
    {
        if (isPlaygroundItem)
        {
            _gameManager.Playground.SetPlaygroundSprites(playgroundItem);
        }
    }

    public void LoadItemStatus(int count = 1, bool isStackable = false)
    {
        //Saplings
        if (count > 1 && isStackable)
        {
            CurrentStackSize = count;
            CurrentItemPrice = ItemPrice * CurrentStackSize;
            _priceUI.SetSpriteNumbers(ItemPrice * CurrentStackSize);
            CurrentItemPrice = ItemPrice * CurrentStackSize;
            if (CurrentStackSize == 6)
            {
                _priceUI.SetSpriteNumbers(0);
            }
            return;
        }
        IsItemBought = true;
        CurrentItemPrice = 0;
        _priceUI.SetSpriteNumbers(CurrentItemPrice);
    }
}
