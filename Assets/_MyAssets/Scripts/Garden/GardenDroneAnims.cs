using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenDroneAnims : MonoBehaviour
{
    [SerializeField] private BerryBush berryBush;
    [SerializeField] private Animator animator;

    private int _gatherAnimHash = Animator.StringToHash("Gather");

    private void Start()
    {
        berryBush.OnBerryGather += BerryBushOnOnBerryGather;
    }

    private void BerryBushOnOnBerryGather()
    {
        animator.CrossFade(_gatherAnimHash, 0,0);
    }
}
