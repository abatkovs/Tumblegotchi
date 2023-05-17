using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private SoundData introSound;

    private void PlayIntroSound()
    {
        
    }

    private void FinishIntro()
    {
        gameObject.SetActive(false);
    }
}
