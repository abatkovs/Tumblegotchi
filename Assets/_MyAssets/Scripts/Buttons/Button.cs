using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    [SerializeField] private Color exitColor = Color.white;
    [SerializeField] private Color enterColor = Color.gray;
    
    [SerializeField] private SpriteRenderer buttonSprite;
    [SerializeField] private Sprite buttonPressedSprite;

    [SerializeField] private bool changeSprite;

    public event Action OnButtonClicked;
    
    public void OnPointerEnter (PointerEventData eventData)
    {
        if (changeSprite)
        {
            buttonSprite.sprite = buttonPressedSprite;
            return;
        }
        buttonSprite.color = enterColor;
        
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        if (changeSprite)
        {
            buttonSprite.sprite = null;
            return;
        }
        buttonSprite.color = exitColor;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClicked?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //buttonSprite.sprite = null;
    }
}
