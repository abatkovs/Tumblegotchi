using System;
using System.Collections;
using System.Collections.Generic;
using _MyAssets.Scripts.Playground;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;

public class PlaygroundAnimator : MonoBehaviour
{
    public enum NextAnimToPlay
    {
        None,
        Wait,
        Play,
        Dizzy,
        Upset,
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

    private bool _lastAnim;
    private PlaygroundItem _playgroundItem;
    private bool _jellyWaitingForInput;


    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        nextAnim = NextAnimToPlay.None;
    }

    private void Update()
    {
        if(nextAnim == NextAnimToPlay.None) return;
        UpdateBoredTimer();
    }

    private void UpdateBoredTimer()
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
    }

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
        if (animationName == _playgroundItem.PlayAnimationString)
        {
            _playgroundItem.TogglePlayedWithJelly(true);
        }
        _animator.enabled = true;
        _animator.CrossFade(animationName,0,0);
    }

    public void FinishAnimation()
    {
        if (_lastAnim)
        {
            PlayStr(_playgroundItem.IdleAnimationString);
            JellyBG.ActivateBGJelly();
            _lastAnim = false;
            nextAnim = NextAnimToPlay.None;
            return;
        }
        if(_jellyWaitingForInput) return;
        PlayNextAnim();
    }
    
    public void RemoveController()
    {
        _animator.runtimeAnimatorController = null;
    }

    public void PlayEnterAnimation(string name)
    {
        PlayStr(name);
    }

    private void PlayNextAnim()
    {
        if (nextAnim == NextAnimToPlay.Wait)
        {
            if (_playgroundItem.IsPlayerInteractionRequired)
            {
                PlayStr(_playgroundItem.WaitAnimationString);
                _jellyWaitingForInput = true;
                IncreaseLove(); //If interacted with jelly increase love
                IncreaseMood();
                return;
            }
            PlayStr(_playgroundItem.WaitAnimationString);
            nextAnim = NextAnimToPlay.Play;
            return;
        }

        if (nextAnim == NextAnimToPlay.Play)
        {
            if (_playgroundItem.IsPlayerInteractionRepeatable)
            {
                PlayStr(_playgroundItem.PlayAnimationString);
                nextAnim = NextAnimToPlay.Wait;
                return;
            }
            PlayStr(_playgroundItem.PlayAnimationString);
            nextAnim = NextAnimToPlay.Dizzy;
            return;
        }

        if (nextAnim == NextAnimToPlay.Dizzy)
        {
            PlayStr(_playgroundItem.DizzyAnimationString);
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
            PlayStr(_playgroundItem.PlayAnimationString);
            _jellyWaitingForInput = false;
            nextAnim = NextAnimToPlay.Wait;
            return;
        }
        nextAnim = NextAnimToPlay.Play;
        _jellyWaitingForInput = false;
    }
}
