using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void RemoveController()
    {
        _animator.runtimeAnimatorController = null;
    }
}
