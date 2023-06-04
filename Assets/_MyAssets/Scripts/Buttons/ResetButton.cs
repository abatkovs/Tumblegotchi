using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Transform tamagotchi;

    [SerializeField] private StartingTransform startingTransform;

    [SerializeField] private StartingTransform minimizedTamagotchiPosition;

    private bool _isSmall;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.OnButtonReleased += Button_OnOnButtonReleased;
        button.OnButtonClicked += Button_OnOnButtonClicked;

        startingTransform.startingScale = tamagotchi.localScale;
        startingTransform.startingPosition = tamagotchi.position;
    }

    private void Button_OnOnButtonClicked()
    {
        Debug.Log("Button Clicked");
    }

    private void Button_OnOnButtonReleased()
    {
        Debug.Log("Resize and move");
        ToggleSize();
    }

    private void ToggleSize()
    {
        if (_isSmall)
        {
            tamagotchi.localScale = startingTransform.startingScale;
            tamagotchi.position = startingTransform.startingPosition;
            _isSmall = false;
        }
        else
        {
            tamagotchi.localScale = minimizedTamagotchiPosition.startingScale;
            tamagotchi.position = minimizedTamagotchiPosition.startingPosition;
            _isSmall = true;
        }
    }
    

    [Serializable]
    protected class StartingTransform
    {
        public Vector3 startingPosition;
        public Vector3 startingScale;
    }
}
