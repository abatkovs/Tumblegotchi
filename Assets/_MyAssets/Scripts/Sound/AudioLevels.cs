using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLevels : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> pips;
    [SerializeField] private int currentSoundLevel;

    private void Start()
    {
        HidePips();
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
}
