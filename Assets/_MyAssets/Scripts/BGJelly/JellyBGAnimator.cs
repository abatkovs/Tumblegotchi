using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBGAnimator : AnimatorCross
{
    private int _idleAnim = Animator.StringToHash("Idle");
    private int _walkAnim = Animator.StringToHash("Walk");

    public void PlayIdleAnim()
    {
        PlayAnim(_idleAnim);
    }

    public void PlayWalkAnim()
    {
        PlayAnim(_walkAnim);
    }
}
