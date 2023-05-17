using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private SoundData introSound;
    [SerializeField] private GameObject game;

    [SerializeField] private SoundManager soundManager;

    private void Start()
    {
        game.SetActive(false);
    }

    private void PlayIntroSound()
    {
        soundManager.PlaySound(introSound);
    }

    private void FinishIntro()
    {
        game.SetActive(true);
        gameObject.SetActive(false);
    }
}
