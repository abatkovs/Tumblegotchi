using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [field: SerializeField] public bool IntroPlaying { get; private set; } = true;
    [SerializeField] private GameObject game;
    [SerializeField] private StudioEventEmitter eventEmitter;
    

    private void Start()
    {
        game.SetActive(false);
    }

    /// <summary>
    /// Animation event
    /// </summary>
    public void FinishIntro()
    {
        game.SetActive(true);
        gameObject.SetActive(false);
        GameManager.Instance.ToggleSelectionButton(false);
        IntroPlaying = false;
        eventEmitter.Stop();
    }
}
