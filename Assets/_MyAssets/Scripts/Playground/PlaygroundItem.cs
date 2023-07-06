using UnityEngine;

namespace _MyAssets.Scripts.Playground
{
    [CreateAssetMenu(fileName = "PlaygroundItem", menuName = "GameData/PlaygroundItem", order = 3)]
    public class PlaygroundItem : ScriptableObject
    {
        [field: SerializeField] public int ItemID { get; private set; }
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        [field: SerializeField] public string IdleAnimationString { get; private set; }
        [field: SerializeField] public string EnterAnimationString { get; private set; }
        [field: SerializeField] public string WaitAnimationString { get; private set; }
        [field: SerializeField] public string PlayAnimationString { get; private set; }
        [field: SerializeField] public string DizzyAnimationString { get; private set; }
        [field: SerializeField] public string UpsetAnimationString { get; private set; }
        [field: Space]
        [field: SerializeField] public bool IsPlayerInteractionRequired { get; private set; }
        [field: SerializeField] public bool IsPlayerInteractionRepeatable { get; private set; }
        [field: SerializeField] public float TimeUntilJellyGetsBored { get; private set; }
        [field: Space] 
        [field: Header("Awarded stat points")]
        [field: SerializeField] public int AwardedLove { get; private set; } = 1;
        [field: SerializeField] public int AwardedMood { get; private set; } = 5;

        private PlaygroundAnimator playgroundAnimator;

        public void SetPlaygroundAnimator(PlaygroundAnimator animator)
        {
            playgroundAnimator = animator;
        }
    }
}