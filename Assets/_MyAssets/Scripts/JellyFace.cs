using System;
using System.Collections.Generic;
using UnityEngine;

public class JellyFace : MonoBehaviour
{

    
    [SerializeField] private FaceState currentJellyFace;
    [SerializeField] private List<JellyFaces> jellyFaces;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if(spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeJellyFace(FaceState newFace)
    {
        currentJellyFace = newFace;
        NewFace();
    }

    private void NewFace()
    {
        foreach (var face in jellyFaces)
        {
            if (face.State == currentJellyFace)
            {
                spriteRenderer.sprite = face.Face;
                return;
            }
        }
    }

}

public enum FaceState
{
    None,
    Neutral,
    Smiling,
    Happy,
    Sad,
}

[Serializable]
public class JellyFaces
{
    public FaceState State;
    public Sprite Face;
}