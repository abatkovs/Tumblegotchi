using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAnimator : AnimatorCross
{
    [SerializeField] private SoundData eatSound;
    [SerializeField] private SoundData dewDropSound;
    [SerializeField] private SoundData pet1;
    [SerializeField] private SoundData pet2;
    [SerializeField] private SoundData swallow;
    [SerializeField] private SoundData evolution;
    
    private int _idleAnim = Animator.StringToHash("Idle");
    private int _feedAnim = Animator.StringToHash("FeedJelly");
    private int _startPettingAnim = Animator.StringToHash("StartPetting");
    private int _pettingAnim = Animator.StringToHash("Petting");
    private int _walkAnim = Animator.StringToHash("Walk");
    private int _walkOnScreenAnim = Animator.StringToHash("WalkOnScreen");
    private int _sleepAnim = Animator.StringToHash("Sleep");
    private int _evolutionAnim = Animator.StringToHash("Evolution");

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
    
    public void PlayWalkOnScreenAnim()
    {
        PlayAnim(_walkOnScreenAnim);
    }
    
    public void PlaySleepAnim()
    {
        PlayAnim(_sleepAnim);
    }

    public void PlayEvolutionAnim()
    {
        PlayAnim(_evolutionAnim);
    }
    
#region sound
    public void PlayEatSound()
    {
        _soundManager.PlaySound(eatSound);
    }

    public void PlayDewDropSound()
    {
        _soundManager.PlaySound(dewDropSound);
    }

    public void PlayPet1Sound()
    {
        _soundManager.PlaySound(pet1);
    }

    public void PlayPet2Sound()
    {
        _soundManager.PlaySound(pet2);
    }

    public void PlaySwallowSound()
    {
        _soundManager.PlaySound(swallow);
    }

    public void PlayEvolutionSound()
    {
        _soundManager.PlaySound(evolution);
    }
#endregion

}
