using UnityEngine;

namespace _MyAssets.Scripts
{
    [CreateAssetMenu(fileName = "BerryBushAge", menuName = "GameData/BerryBush", order = 0)]
    public class BerryBushState : ScriptableObject
    {
        [field: SerializeField] public float AgeThreshold { get; private set; }
        [field: SerializeField] public float Sprite { get; private set; }
    }
}