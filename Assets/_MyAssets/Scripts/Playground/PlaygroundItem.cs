using UnityEngine;

namespace _MyAssets.Scripts.Playground
{
    [CreateAssetMenu(fileName = "PlaygroundItem", menuName = "GameData/PlaygroundItem", order = 3)]
    public class PlaygroundItem : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
    }
}