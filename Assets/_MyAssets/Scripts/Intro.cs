using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject game;

    private void Start()
    {
        game.SetActive(false);
    }

    /// <summary>
    /// Animation event
    /// </summary>
    private void FinishIntro()
    {
        game.SetActive(true);
        gameObject.SetActive(false);
    }
}
