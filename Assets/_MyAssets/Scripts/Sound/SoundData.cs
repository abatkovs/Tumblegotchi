using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SoundData", menuName = "GameData/SoundData", order = 0)]
public class SoundData : ScriptableObject
{
    [field: FormerlySerializedAs("sound")] [field: SerializeField] public Sound Sound { get; private set; }
    [field: SerializeField] public EventReference SoundEvent { get; private set; }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public bool loop = false;
}