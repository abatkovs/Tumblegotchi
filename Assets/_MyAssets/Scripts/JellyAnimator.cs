using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAnimator : MonoBehaviour
{

    private Animator _animator;

    private int _idleAnim = Animator.StringToHash("Idle");
    private int _feedAnim = Animator.StringToHash("FeedJelly");
    private int _startPettingAnim = Animator.StringToHash("StartPetting");
    private int _pettingAnim = Animator.StringToHash("Petting");
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.CrossFade(_idleAnim,0,0);    
    }

    public void PlayIdleAnim()
    {
        _animator.CrossFade(_idleAnim,0,0);
    }

    public void PlayFeedAnim()
    {
        _animator.CrossFade(_feedAnim,0,0);
    }

    public void PlayStartPettingAnim()
    {
        _animator.CrossFade(_startPettingAnim,0,0);
    }

    public void PlayPettingAnim()
    {
        _animator.CrossFade(_pettingAnim,0,0);
    }
}
