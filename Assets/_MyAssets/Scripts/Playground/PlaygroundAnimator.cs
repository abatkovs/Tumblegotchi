using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundAnimator : AnimatorCross
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void RemoveController()
    {
        _animator.runtimeAnimatorController = null;
    }
}
