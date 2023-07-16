using System;
using System.Collections.Generic;
using UnityEngine;

public class JellyFace : MonoBehaviour
{

    [SerializeField] private JellyStats jellyStats;
    [SerializeField] private Jelly jelly;
    [SerializeField] private FaceState currentJellyFace;
    [SerializeField] private List<JellyFaces> jellyFaces;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        jellyStats.OnMoodChange += JellyStatsOnOnMoodChange;
        jellyStats.OnJellyHideToggleFace += JellyStatsOnOnJellyHideToggleFace;
        jelly.OnFinishComingBack += JellyOnOnFinishComingBack;
        jelly.OnStartWalkingToBG += JellyOnOnStartWalkingToBG;
        if(spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void JellyOnOnStartWalkingToBG()
    {
        spriteRenderer.enabled = false;
    }

    private void JellyOnOnFinishComingBack()
    {
        spriteRenderer.enabled = true;
    }

    private void JellyStatsOnOnJellyHideToggleFace(bool value)
    {
        spriteRenderer.enabled = !value;
    }

    private void JellyStatsOnOnMoodChange(JellyStats.JellyMood jellyMood)
    {
        switch (jellyMood)
        {
            case JellyStats.JellyMood.Sad:
                ChangeJellyFace(FaceState.Sad);
                break;
            case JellyStats.JellyMood.Smiling:
                ChangeJellyFace(FaceState.Smiling);
                break;
            case JellyStats.JellyMood.Neutral:
                ChangeJellyFace(FaceState.Neutral);
                break;
            case JellyStats.JellyMood.Happy:
                ChangeJellyFace(FaceState.Happy);
                break;
            default:
                ChangeJellyFace(FaceState.Happy);
                break;
        }
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