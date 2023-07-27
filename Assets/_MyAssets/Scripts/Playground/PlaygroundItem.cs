﻿using UnityEngine;

namespace _MyAssets.Scripts.Playground
{
    [CreateAssetMenu(fileName = "PlaygroundItem", menuName = "GameData/PlaygroundItem", order = 3)]
    public class PlaygroundItem : ScriptableObject
    {
        [field: SerializeField] public int ItemID { get; private set; }
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        [field: SerializeField] public AnimationClip IdleAnimation { get; private set; }
        [field: SerializeField] public AnimationClip EnterAnimation { get; private set; }
        [field: SerializeField] public AnimationClip WaitPlayerInputAnimation { get; private set; }
        [field: SerializeField] public AnimationClip PlayWithJellyAnimation { get; private set; }
        [field: SerializeField] public AnimationClip DizzyAnimation { get; private set; }
        [field: SerializeField] public AnimationClip UpsetAnimation { get; private set; }
        [field: SerializeField] public AnimationClip ExitAnimation { get; private set; }
        [field: Space]
        [field: SerializeField] public bool IsPlayerInteractionRequired { get; private set; }
        [field: SerializeField] public bool IsPlayerInteractionRepeatable { get; private set; }
        [field: SerializeField] public float TimeUntilJellyGetsBored { get; private set; }
        [field: Space] 
        [field: Header("Awarded stat points")]
        [field: SerializeField] public int AwardedLove { get; private set; } = 1;
        [field: SerializeField] public int AwardedMood { get; private set; } = 5;
        [field: SerializeField] public int DecreasedMoodIfNoInteraction { get; private set; } = -5;
        [field: SerializeField] public Transform ExitPoint { get; private set; }

        private PlaygroundAnimator _playgroundAnimator;
        private bool _playedWithJelly;

        public void InitPlayingValues()
        {
            TogglePlayedWithJelly(false);
        }

        public void TogglePlayedWithJelly(bool value)
        {
            _playedWithJelly = value;
        }

        public bool DidPlayerPlayWithJelly()
        {
            return _playedWithJelly;
        }

        public void SetPlaygroundAnimator(PlaygroundAnimator animator)
        {
            _playgroundAnimator = animator;
        }

        public void SetExitTransform(Transform exitTransform)
        {
            ExitPoint = exitTransform;
        }
    }
}