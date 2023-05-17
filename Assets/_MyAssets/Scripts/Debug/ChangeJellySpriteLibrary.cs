using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;

public class ChangeJellySpriteLibrary : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private SpriteLibrary spriteLibrary;
    [SerializeField] private List<SpriteLibraryAsset> spriteLibraries;

    [SerializeField] private int selectedLibrary;
    
    private void Start()
    {
        button.OnButtonClicked += OnChangeJellySpriteLibrary;
    }

    private void OnChangeJellySpriteLibrary()
    {
        var libCount = spriteLibraries.Count;
        
        selectedLibrary++;
        if (selectedLibrary % libCount == 0)
        {
            selectedLibrary = 0;
        }
        
        spriteLibrary.spriteLibraryAsset = spriteLibraries[selectedLibrary];
    }
}
