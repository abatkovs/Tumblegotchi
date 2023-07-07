using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "JellyMoodData", menuName = "GameData/JellyMoodData", order = 10)]
public class MoodData : ScriptableObject
{
    [field: SerializeField] public float MinRandomIntervalForAction { get; private set; } = 20f;
    [field: SerializeField] public float MaxRandomIntervalForAction { get; private set; } = 50f;
    [field: SerializeField] [ItemCanBeNull] public List<JellyMoodActions> JellyMoodActions { get; private set; }
    
}

[System.Serializable]
public class JellyMoodActions
{
    [SerializeField] private string actionName;
    [field: SerializeField] public AnimationClip AnimationClip { get; private set; }
    [field: SerializeField] public SoundData AudioClip { get; private set; }
}