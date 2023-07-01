using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
//TODO: add some timers for holding down button etc.
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

    [Space(20)] 
    [SerializeField] private bool saveGame;

    

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

    /// <summary>
    /// Button clicked
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClicked?.Invoke();
        if(saveGame) SaveManager.Instance.SaveGame();
    }

    /// <summary>
    /// Button pressed down
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localPosition += (Vector3) posOffset;
        OnButtonDown?.Invoke();
    }
    
    /// <summary>
    /// Button released
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonReleased?.Invoke();
        
    }




}
