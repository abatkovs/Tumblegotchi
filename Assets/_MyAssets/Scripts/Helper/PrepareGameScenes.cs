using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

/// <summary>
/// Hide gameobjects that should be hidden
/// </summary>
public class PrepareGameScenes : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjectsThatNeedToBeHidden; 
    
    [Button(InspectorButtonSize.Large)]
    private void HideGameObjects()
    {
        ToggleGameObjects(false);
    }
    
    [Button(InspectorButtonSize.Small)]
    private void ShowGameObjects()
    {
        ToggleGameObjects(true);
    }
    
    private void ToggleGameObjects(bool value)
    {
        foreach (var go in gameObjectsThatNeedToBeHidden)
        {
            go.SetActive(value);
        }
    }
}
