using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBGAnimator : AnimatorCross
{
    private int _idleAnim = Animator.StringToHash("Idle");
    private int _walkAnim = Animator.StringToHash("Walk");
    private int _walkInAnim = Animator.StringToHash("WalkIn");

    public void PlayIdleAnim()
    {
        PlayAnim(_idleAnim);
    }

    public void PlayWalkAnim()
    {
        PlayAnim(_walkAnim);
    }

    public void PlayWalkInAnim()
    {
        PlayAnim(_walkInAnim);
    }
}
