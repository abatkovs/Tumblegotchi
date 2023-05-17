using UnityEngine;

namespace _MyAssets.Scripts.Playground
{
    [CreateAssetMenu(fileName = "PlaygroundItem", menuName = "GameData/PlaygroundItem", order = 3)]
    public class PlaygroundItem : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        [field: SerializeField] public string EnterAnimationString { get; private set; }
        [field: SerializeField] public string WaitAnimationString { get; private set; }
        [field: SerializeField] public string PlayAnimationString { get; private set; }
        [field: SerializeField] public string DizzyAnimationString { get; private set; }
        [field: SerializeField] public string UpsetAnimationString { get; private set; }
    }
}