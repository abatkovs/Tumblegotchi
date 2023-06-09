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
    [SerializeField] private SoundData selectionSound;
    [Space(25)]
    [SerializeField] private MenuOptions selectedAction = MenuOptions.Garden;

    private SoundManager _soundManager;
    private GameManager _gameManager;
    private void Start()
    {
        ClearSelection();
        _soundManager = SoundManager.Instance;
        _gameManager = GameManager.Instance;
        selectedAction = _gameManager.CurrentlySelectedMenuOption;
        SetSelection(selectedAction);
        
        _gameManager.OnSceneSwitch += GM_OnOnSceneSwitch;
    }
    
    private void OnDestroy()
    {
        _gameManager.OnSceneSwitch -= GM_OnOnSceneSwitch;
    }

    private void GM_OnOnSceneSwitch(ActiveScene sceneSwitchedTo)
    {
        if (sceneSwitchedTo != ActiveScene.Main)
        {
            ClearSelection();
            return;
        }
        
        ClearSelection();
        SetSelection(_gameManager.CurrentlySelectedMenuOption);
    }

    private void ClearSelection()
    {
        foreach (var sprite in selectionSprites)
        {
            if(sprite == null) continue;
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
        if ((int) selectedAction >= selectionCount) selectedAction = MenuOptions.Garden;
        SetSelection(selectedAction);
    }
}
