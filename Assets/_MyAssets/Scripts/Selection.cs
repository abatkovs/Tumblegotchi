using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class Selection : MonoBehaviour
{
    [SerializeField] private Color activeColor = Color.black;
    [SerializeField] private Color inactiveColor = Color.white;
    [SerializeField] private List<SpriteRenderer> selectionSprites;

    [Space(25)]
    [SerializeField] private MenuOptions selectedAction;
    
    
    private void Start()
    {
        ClearSelection();
        selectedAction = GameManager.Instance.CurrentlySelectedMenuOption;
        SetSelection(selectedAction);
    }

    private void ClearSelection()
    {
        foreach (var sprite in selectionSprites)
        {
            sprite.color = inactiveColor;
        }
    }

    private void SetSelection(MenuOptions selection)
    {
        ClearSelection();
        selectionSprites[(int)selection].color = activeColor;
        GameManager.Instance.SwitchActiveMenuSelection(selection);
    }

    /// <summary>
    /// Cycle over menu options
    /// </summary>
    public void CycleSelection()
    {
        selectedAction++;
        var selectionCount = selectionSprites.Count;
        if ((int) selectedAction >= selectionCount) selectedAction = 0;
        SetSelection(selectedAction);
    }

}
