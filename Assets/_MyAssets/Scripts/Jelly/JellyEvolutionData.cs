using UnityEngine;
using UnityEngine.U2D.Animation;

namespace _MyAssets.Scripts.Jelly
{
    [CreateAssetMenu(fileName = "Evolution", menuName = "GameData/Evolutions", order = 0)]
    public class JellyEvolutionData : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public SpriteLibraryAsset Egg { get; private set; }
        [field: SerializeField] public SpriteLibraryAsset Baby { get; private set; }
        [field: SerializeField] public SpriteLibraryAsset Young { get; private set; }
        [field: SerializeField] public SpriteLibraryAsset Adult { get; private set; }
        
    }
}