using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Animator sceneTransitionAnimator;

    private int _mainToGardenTrigger = Animator.StringToHash("ToGarden");
    private int _gardenToMain = Animator.StringToHash("GardenToMain");
    
    [SerializeField] private BaseScene mainScene;
    [SerializeField] private BaseScene gardenScene;

    private void Start()
    {
        if (sceneTransitionAnimator == null) sceneTransitionAnimator = GetComponent<Animator>();
    }

    public void SwitchScene()
    {
        if (GameManager.Instance.CurrentlySelectedMenuOption == MenuOptions.Garden)
        {
            sceneTransitionAnimator.SetTrigger(_mainToGardenTrigger);
            GameManager.Instance.SwitchActiveScene(ActiveScene.Garden);
        }
    }

    public void BackToMain()
    {
        if (GameManager.Instance.CurrentlyActiveScene == ActiveScene.Garden)
        {
            sceneTransitionAnimator.SetTrigger(_gardenToMain);
            GameManager.Instance.SwitchActiveScene(ActiveScene.Main);
        }
    }
}
