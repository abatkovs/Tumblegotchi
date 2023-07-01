using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DERECInvasion : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float timer;
    [SerializeField] private float invasionInterval = 60f;

    private int _flyByAnimHash = Animator.StringToHash("FlyBy");
    
    private void Update()
    {
        UpdateInvasionTimer();
    }

    private void UpdateInvasionTimer()
    {
        timer += Time.deltaTime;
        if (timer > invasionInterval)
        {
            timer = 0;
            animator.CrossFade(_flyByAnimHash,0,0);
        }
    }
}
