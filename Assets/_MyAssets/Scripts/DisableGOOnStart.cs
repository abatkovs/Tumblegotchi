using UnityEngine;

/// <summary>
/// Mostly used to disable local masks for sprites that use them.
/// </summary>
public class DisableGOOnStart : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    private void Start()
    {
        DisableGameObjects();
    }

    private void DisableGameObjects()
    {
        foreach (var go in gameObjects)
        {
            go.SetActive(false);
        }
    }
}
