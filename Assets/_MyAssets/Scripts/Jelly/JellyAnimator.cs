using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAnimator : AnimatorCross
{
    [SerializeField] private SoundData eatSound;
    [SerializeField] private SoundData dewDropSound;
    
    private int _idleAnim = Animator.StringToHash("Idle");
    private int _feedAnim = Animator.StringToHash("FeedJelly");
    private int _startPettingAnim = Animator.StringToHash("StartPetting");
    private int _pettingAnim = Animator.StringToHash("Petting");
    private int _walkAnim = Animator.StringToHash("Walk");

    private SoundManager _soundManager;
    void Start()
    {
        _soundManager = SoundManager.Instance;
        PlayAnim(_idleAnim);   
    }

    public void PlayIdleAnim()
    {
        PlayAnim(_idleAnim);
    }

    public void PlayFeedAnim()
    {
        PlayAnim(_feedAnim);
    }

    public void PlayStartPettingAnim()
    {
        PlayAnim(_startPettingAnim);
    }

    public void PlayPettingAnim()
    {
        PlayAnim(_pettingAnim);
    }
    
    public void PlayWalkOffScreenAnim()
    {
        PlayAnim(_walkAnim);
    }

    public void PlayEatSound()
    {
        _soundManager.PlaySound(eatSound);
    }

    public void PlayDewDropSound()
    {
        _soundManager.PlaySound(dewDropSound);
    }
}
