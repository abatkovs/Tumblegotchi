using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaygroundAnimator : MonoBehaviour
{
    public enum NextAnimToPlay
    {
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
    
    public string WaitAnimationString { get; set; }
    public string PlayAnimationString { get; set; }
    public string DizzyAnimationString { get; set; }
    public string UpsetAnimationString { get; set; }

    private bool _lastAnim;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    public void PlayStr(string animationName)
    {
        _animator.enabled = true;
        _animator.CrossFade(animationName,0,0);
    }

    public void FinishAnimation()
    {
        _animator.enabled = false;
        if (_lastAnim)
        {
            spriteRenderer.sprite = sprite;
            JellyBG.ActivateJelly();
            return;
        }
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
            PlayStr(WaitAnimationString);
            nextAnim = NextAnimToPlay.Play;
            return;
        }

        if (nextAnim == NextAnimToPlay.Play)
        {
            PlayStr(PlayAnimationString);
            nextAnim = NextAnimToPlay.Dizzy;
            return;
        }

        if (nextAnim == NextAnimToPlay.Dizzy)
        {
            PlayStr(DizzyAnimationString);
            _lastAnim = true;
            spriteRenderer.sprite = sprite;
            JellyBG.ActivateJelly();
            return;
        }

    }

    public void SetSprite(Sprite itemSprite)
    {
        spriteRenderer.sprite = itemSprite;
        sprite = itemSprite;
    }
}
