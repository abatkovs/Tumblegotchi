using UnityEngine;

namespace _MyAssets.Scripts.Playground
{
    [CreateAssetMenu(fileName = "PlaygroundData", menuName = "GameData/Playground", order = 0)]
    public class PlaygroundData : ScriptableObject
    {
        public Sprite sprite { get; private set; }
    }
}