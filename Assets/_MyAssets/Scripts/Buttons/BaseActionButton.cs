using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActionButton : MonoBehaviour
{
    [SerializeField] protected Button button;
    
    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        button.OnButtonClicked += Button_OnOnButtonClicked;
    }
    
    private void Button_OnOnButtonClicked()
    {
        Action();
    }

    protected abstract void Action();

    private void OnDestroy()
    {
        button.OnButtonClicked -= Button_OnOnButtonClicked;
    }
}
