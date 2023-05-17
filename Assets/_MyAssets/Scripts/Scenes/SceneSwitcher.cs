using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private JellyAnimator jellyAnimator;
    [Space]
    [SerializeField] private SoundData screenLeftSound;
    [SerializeField] private SoundData screenRightSound;

    private int _mainToGardenAnim = Animator.StringToHash("MainToGarden");
    private int _gardenToMainAnim = Animator.StringToHash("GardenToMain");
    private int _mainToShopAnim = Animator.StringToHash("MainToShop");
    private int _shopToMainAnim = Animator.StringToHash("ShopToMain");
    
    private GameManager _gameManager;
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        _gameManager = GameManager.Instance;
        if (sceneTransitionAnimator == null) sceneTransitionAnimator = GetComponent<Animator>();
    }

    public void SwitchScene()
    {
        MainToGarden();

        MainToShop();
    }

    private void MainToShop()
    {
        if (_gameManager.CurrentlySelectedMenuOption == MenuOptions.Shop)
        {
            sceneTransitionAnimator.CrossFade(_mainToShopAnim, 0, 0);
            _gameManager.SwitchActiveScene(ActiveScene.Shop);
            _gameManager.LockButtons = true;
            _soundManager.PlaySound2(screenLeftSound);
        }
    }

    private void MainToGarden()
    {
        if (_gameManager.CurrentlySelectedMenuOption == MenuOptions.Garden)
        {
            sceneTransitionAnimator.CrossFade(_mainToGardenAnim, 0, 0);
            _gameManager.SwitchActiveScene(ActiveScene.Garden);
            _gameManager.LockButtons = true;
            _soundManager.PlaySound2(screenRightSound);
        }
    }

    public void SwitchToPlayground()
    {
        _gameManager.SwitchActiveScene(ActiveScene.Playground);
        jellyAnimator.PlayWalkOffScreenAnim();
    }
    
    public void PlaygroundToMain()
    {
        
    }

    public void BackToMain()
    {
        GardenToMain();
        ShopToMain();
    }

    private void ShopToMain()
    {
        if (_gameManager.CurrentlyActiveScene == ActiveScene.Shop)
        {
            sceneTransitionAnimator.CrossFade(_shopToMainAnim, 0, 0);
            _gameManager.SwitchActiveMenuSelection(MenuOptions.Shop);
            _gameManager.SwitchActiveScene(ActiveScene.Main);
            _gameManager.LockButtons = true;
            _soundManager.PlaySound2(screenRightSound);
        }
    }

    private void GardenToMain()
    {
        if (_gameManager.CurrentlyActiveScene == ActiveScene.Garden)
        {
            sceneTransitionAnimator.CrossFade(_gardenToMainAnim, 0, 0);
            _gameManager.SwitchActiveMenuSelection(MenuOptions.Garden);
            _gameManager.SwitchActiveScene(ActiveScene.Main);
            _gameManager.LockButtons = true;
            _soundManager.PlaySound2(screenLeftSound);
        }
    }

    public void FinishedSwitchingScene()
    {
        _gameManager.UpdateSelection();
        _gameManager.LockButtons = false;
    }
}
