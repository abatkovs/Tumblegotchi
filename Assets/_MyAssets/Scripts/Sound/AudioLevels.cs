using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLevels : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> pips;
    [SerializeField] private int currentSoundLevel;
        
    [Space]
    [SerializeField] private float visibilityTimer = 2f;
    [SerializeField] private float timer;
    
    private void Start()
    {
        HidePips();
        gameObject.SetActive(false);
    }
    
    
    private void Update()
    {
        CountDownTimer();
        if (timer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void HidePips()
    {
        foreach (var pip in pips)
        {
            pip.enabled = false;
        }
    }

    public void ChangeSoundLevel(int amount)
    {
        currentSoundLevel = Mathf.Clamp(currentSoundLevel + amount, 0, pips.Count);
        HidePips();
        for (int i = 0; i < currentSoundLevel; i++)
        {
            pips[i].enabled = true;
        }
    }
    
    public void StartCountdown()
    {
        gameObject.SetActive(true);
        timer = visibilityTimer;
    }

    private void CountDownTimer()
    {
        timer -= Time.deltaTime;
    }
}
