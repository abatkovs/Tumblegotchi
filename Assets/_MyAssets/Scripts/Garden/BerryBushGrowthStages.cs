using UnityEngine;

[CreateAssetMenu(fileName = "BerryBushAge", menuName = "GameData/BerryBush", order = 0)]
public class BerryBushGrowthStages : ScriptableObject
{
    [field: SerializeField] public float AgeThreshold { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int BerryYield { get; private set; }
    [field: SerializeField] public bool IsFullyGrown { get; private set; }
}

