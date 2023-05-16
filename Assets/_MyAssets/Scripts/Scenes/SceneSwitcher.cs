using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private JellyAnimator jellyAnimator;

    private int _mainToGardenAnim = Animator.StringToHash("MainToGarden");
    private int _gardenToMainAnim = Animator.StringToHash("GardenToMain");
    private int _mainToShopAnim = Animator.StringToHash("MainToShop");
    private int _shopToMainAnim = Animator.StringToHash("ShopToMain");
    
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        if (sceneTransitionAnimator == null) sceneTransitionAnimator = GetComponent<Animator>();
    }

    public void SwitchScene()
    {
        if (_gameManager.CurrentlySelectedMenuOption == MenuOptions.Garden)
        {
            sceneTransitionAnimator.CrossFade(_mainToGardenAnim,0,0);
            _gameManager.SwitchActiveScene(ActiveScene.Garden);
            _gameManager.LockButtons = true;
        }

        if (_gameManager.CurrentlySelectedMenuOption == MenuOptions.Shop)
        {
            sceneTransitionAnimator.CrossFade(_mainToShopAnim,0,0);
            _gameManager.SwitchActiveScene(ActiveScene.Shop);
            _gameManager.LockButtons = true;
        }
    }

    public void SwitchToPlayground()
    {
        _gameManager.SwitchActiveScene(ActiveScene.Playground);
        jellyAnimator.PlayWalkOffScreenAnim();
    }

    public void BackToMain()
    {
        if (_gameManager.CurrentlyActiveScene == ActiveScene.Garden)
        {
            sceneTransitionAnimator.CrossFade(_gardenToMainAnim,0,0);
            _gameManager.SwitchActiveMenuSelection(MenuOptions.Garden);
            _gameManager.SwitchActiveScene(ActiveScene.Main);
            _gameManager.LockButtons = true;
        }

        if (_gameManager.CurrentlyActiveScene == ActiveScene.Shop)
        {
            sceneTransitionAnimator.CrossFade(_shopToMainAnim,0,0);
            _gameManager.SwitchActiveMenuSelection(MenuOptions.Shop);
            _gameManager.SwitchActiveScene(ActiveScene.Main);
            _gameManager.LockButtons = true;
        }
    }

    public void FinishedSwitchingScene()
    {
        _gameManager.UpdateSelection();
        _gameManager.LockButtons = false;
    }
}
