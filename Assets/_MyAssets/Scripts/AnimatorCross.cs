using System;
using UnityEngine;

public class AnimatorCross : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnim(int animToPlay)
    {
        _animator.CrossFade(animToPlay,0,0);
    }

    public void PlayAnim(string animString)
    {
        _animator.CrossFade(animString, 0,0);
    }
}
