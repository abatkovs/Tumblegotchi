using System;
using System.Collections;
using System.Collections.Generic;
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

    private Vector2 _startingPosition;

    public event Action OnButtonClicked;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        if(changePosition) return;
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
            ResetPosition();
            return;
        }
        
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

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localPosition += (Vector3) posOffset;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = _startingPosition;
    }


}
