using System;
using System.Collections;
using System.Collections.Generic;
using _MyAssets.Scripts.Playground;
using UnityEngine;
using UnityEngine.U2D.Animation;

//TODO: Not really an animator...
public class PlaygroundAnimator : MonoBehaviour
{
    public enum NextAnimToPlay
    {
        None,
        Wait,
        Play,
        Dizzy,
        Upset,
        Exit,
    }
    private Animator _animator;

    [SerializeField] private NextAnimToPlay nextAnim = NextAnimToPlay.Wait;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    [SerializeField] public JellyBG JellyBG;
    [SerializeField] private JellyStats jellyStats;
    
    [SerializeField] private float timeUntilJellyWillGetBored;
    [SerializeField] private float currentBoredTimer;

    [SerializeField] private SpriteLibrary spriteLibrary;

    [Space]
    [SerializeField] private Transform exitPoint;

    private bool _lastAnim;
    private PlaygroundItem _playgroundItem;
    private bool _jellyWaitingForInput;


    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        nextAnim = NextAnimToPlay.None;
    }

    private void Update()
    {
        if(nextAnim == NextAnimToPlay.None) return;
        UpdateBoredTimer();
        CheckIfJellyIsBored();
    }

    private void UpdateBoredTimer()
    {
        currentBoredTimer -= Time.deltaTime;
    }

    private void CheckIfJellyIsBored()
    {
        if (currentBoredTimer < 0)
        {
            _jellyWaitingForInput = false;
            nextAnim = NextAnimToPlay.Dizzy;
            if(!_playgroundItem.DidPlayerPlayWithJelly())
            {
                jellyStats.ChangeMoodLevel(_playgroundItem.DecreasedMoodIfNoInteraction);
                nextAnim = NextAnimToPlay.Upset;
                return;
            }
            if (_playgroundItem.DidPlayerPlayWithJelly())
            {
                IncreaseLove();
                IncreaseMood();
            }
        }
    }

    /*private void UpdateBoredTimer()
    {
        currentBoredTimer -= Time.deltaTime;
        if (currentBoredTimer < 0)
        {
            _jellyWaitingForInput = false;
            nextAnim = NextAnimToPlay.Dizzy;
            if(!_playgroundItem.DidPlayerPlayWithJelly())
            {
              jellyStats.ChangeMoodLevel(_playgroundItem.DecreasedMoodIfNoInteraction);
              return;
            }
            if (_jellyWaitingForInput)
            {
                IncreaseLove();
                IncreaseMood();
            }
        }
    }*/

    private void IncreaseLove()
    {
        jellyStats.IncreaseLove(_playgroundItem.AwardedLove);
    }

    private void IncreaseMood()
    {
        jellyStats.ChangeMoodLevel(_playgroundItem.AwardedMood);
    }

    public void SetJellyBoredTimers(float time)
    {
        timeUntilJellyWillGetBored = time;
        currentBoredTimer = timeUntilJellyWillGetBored;
    }

    public void PlayStr(string animationName)
    {
        spriteLibrary.spriteLibraryAsset = JellyBG.GetSpriteLibAsset();
        if (animationName == _playgroundItem.PlayWithJellyAnimation.name)
        {
            _playgroundItem.TogglePlayedWithJelly(true); //Player interacted with jelly
            if (_playgroundItem.IsPlayerInteractionRepeatable)
            {
                _playgroundItem.ToggleAnimationLoop(true);
            }
        }
        _animator.enabled = true;
        _animator.CrossFade(animationName,0,0);
    }

    public void FinishAnimation()
    {
        if (_lastAnim)
        {
            PlayStr(_playgroundItem.IdleAnimation.name);
            JellyBG.ExitPlayground(_playgroundItem.ExitPoint);
            _lastAnim = false;
            nextAnim = NextAnimToPlay.None;
            return;
        }
        if(_jellyWaitingForInput) return;
        PlayNextAnim();
    }

    public void PlayEnterAnimation(string name)
    {
        PlayStr(name);
    }

    private void ExitAnimation()
    {
        
    }

    private void PlayNextAnim()
    {
        if (nextAnim == NextAnimToPlay.Wait)
        {
            if (_playgroundItem.IsPlayerInteractionRequired)
            {
                PlayStr(_playgroundItem.WaitPlayerInputAnimation.name);
                _jellyWaitingForInput = true;
                IncreaseLove(); //If interacted with jelly increase love
                IncreaseMood();
                return;
            }
            PlayStr(_playgroundItem.WaitPlayerInputAnimation.name);
            nextAnim = NextAnimToPlay.Play;
            return;
        }

        if (nextAnim == NextAnimToPlay.Play)
        {
            if (_playgroundItem.IsPlayerInteractionRepeatable)
            {
                PlayStr(_playgroundItem.PlayWithJellyAnimation.name);
                if (_playgroundItem.ShouldAnimationLoop())
                {
                    nextAnim = NextAnimToPlay.Play;
                    _playgroundItem.ToggleAnimationLoop(false);
                    return;
                }
                nextAnim = NextAnimToPlay.Wait;
                return;
            }
            PlayStr(_playgroundItem.PlayWithJellyAnimation.name);
            nextAnim = NextAnimToPlay.Dizzy;
            return;
        }

        if (nextAnim == NextAnimToPlay.Dizzy)
        {
            PlayStr(_playgroundItem.DizzyAnimation.name);
            _playgroundItem.SetExitTransform(exitPoint);
            _lastAnim = true;
            spriteRenderer.sprite = sprite;
            return;
        }

        if (nextAnim == NextAnimToPlay.Upset)
        {
            PlayStr(_playgroundItem.UpsetAnimation.name);
            _playgroundItem.SetExitTransform(exitPoint);
            _lastAnim = true;
            spriteRenderer.sprite = sprite;
            return;
        }

    }

    public void SetSprite(Sprite itemSprite)
    {
        spriteRenderer.sprite = itemSprite;
        sprite = itemSprite;
    }

    public void ResetNextAnim()
    {
        nextAnim = NextAnimToPlay.Wait;
    }

    public void SetPlaygroundItem(PlaygroundItem playgroundItem)
    {
        _playgroundItem = playgroundItem;
    }

    public void PlayWithJelly()
    {
        if(!_jellyWaitingForInput) return;
        currentBoredTimer = timeUntilJellyWillGetBored;
        //Start playing instantly
        if (_playgroundItem.IsPlayerInteractionRepeatable)
        {
            PlayStr(_playgroundItem.PlayWithJellyAnimation.name);
            _jellyWaitingForInput = false;
            if (_playgroundItem.ShouldAnimationLoop())
            {
                nextAnim = NextAnimToPlay.Play;
                _playgroundItem.ToggleAnimationLoop(false);
                return;
            }
            nextAnim = NextAnimToPlay.Wait;
            return;
        }
        nextAnim = NextAnimToPlay.Play;
        _jellyWaitingForInput = false;
    }
}
