using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCrystal : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private SoundData sound;
    
    private int _idleAnim = Animator.StringToHash("Idle");
    private int _activeAnim = Animator.StringToHash("Activated");

    private Animator _animator;
    private GameManager _gameManager;
    private SoundManager _soundManager;
    
    private void Start()
    {
        if (_soundManager == null) _soundManager = SoundManager.Instance;
        if (_gameManager == null) _gameManager = GameManager.Instance;
        if (_animator == null) _animator = GetComponent<Animator>();
        shop.OnItemBuy += Shop_OnOnItemBuy;
    }

    private void Shop_OnOnItemBuy()
    {
        _soundManager.PlaySound(sound);
        _animator.CrossFade(_activeAnim,0,0);
        _gameManager.LockButtons = true;
    }

    public void FinishAnimation()
    {
        _animator.CrossFade(_idleAnim,0,0);
        _gameManager.LockButtons = false;
    }
}
