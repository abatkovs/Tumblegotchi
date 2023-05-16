using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SoundData", menuName = "GameData/SoundData", order = 0)]
public class SoundData : ScriptableObject
{
    [field: FormerlySerializedAs("sound")] [field: SerializeField] public Sound Sound { get; private set; }
    [field: Range(0f, 1f)] 
    [field: SerializeField] public float Volume { get; private set; } = 1f;
    [field: Range(-3f, 3f)]
    [field: SerializeField] public float Pitch { get; private set; } = 1f;
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public bool loop = false;
}