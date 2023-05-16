using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoneyButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.OnButtonClicked += Button_OnOnButtonClicked;
    }

    private void Button_OnOnButtonClicked()
    {
        GameManager.Instance.AddJellyDew(10);
    }
}
