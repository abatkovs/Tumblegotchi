using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField] private Color activeColor = Color.black;
    [SerializeField] private Color inactiveColor = Color.white;
    [SerializeField] private List<SpriteRenderer> selectionSprites;

    private int _selectedAction;
    
    
    private void Start()
    {
        ClearSelection();
        SetSelection(_selectedAction);
    }

    [ContextMenu("Clear")]
    private void ClearSelection()
    {
        foreach (var sprite in selectionSprites)
        {
            sprite.color = Color.white;
        }
    }

    private void SetSelection(int selection)
    {
        ClearSelection();
        selectionSprites[selection].color = activeColor;
    }

    public void CycleSelection()
    {
        _selectedAction++;
        var selectionCount = selectionSprites.Count;
        if (_selectedAction >= selectionCount) _selectedAction = 0;
        SetSelection(_selectedAction);
    }

}