using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Color exitColor = Color.white;
    [SerializeField] private Color enterColor = Color.gray;
    
    [SerializeField] private SpriteRenderer buttonSprite;
    [SerializeField] private Sprite buttonPressedSprite;

    [SerializeField] private bool changeSprite;
    [SerializeField] private bool changePosition;
    [SerializeField] private bool changeColor;

    [SerializeField] private Vector2 posOffset;

    

    public event Action OnButtonClicked;
    public event Action OnButtonReleased;
    public event Action OnButtonDown;
    
    public void OnPointerEnter (PointerEventData eventData)
    {
        if(changePosition) return;
        if(buttonSprite == null) return;
        if (changeSprite)
        {
            buttonSprite.sprite = buttonPressedSprite;
            return;
        }
        buttonSprite.color = enterColor;
        
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        if (changePosition)
        {
            return;
        }
        if(buttonSprite == null) return;
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
        SaveManager.Instance.SaveGame();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localPosition += (Vector3) posOffset;
        OnButtonDown?.Invoke();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonReleased?.Invoke();
        
    }




}
