using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color exitColor = Color.white;
    [SerializeField] private Color enterColor = Color.gray;
    
    [SerializeField] private SpriteRenderer buttonSprite;

    public event Action OnButtonClicked;
    
    public void OnPointerEnter (PointerEventData eventData)
    {
        
        buttonSprite.color = enterColor;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        buttonSprite.color = exitColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClicked?.Invoke();
    }
}
