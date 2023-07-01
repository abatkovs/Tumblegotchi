using System;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [Space]
    [SerializeField] private Transform tamagotchi;
    [SerializeField] private StartingTransform startingTransform;
    [SerializeField] private StartingTransform minimizedTamagotchiPosition;
    [Space] 
    [SerializeField] private float resetTimer;
    [SerializeField] private float afterHowLongWillGameReset = 30f;
    [SerializeField] private float afterHowLongGameWillShrink = 1f; //after how long of holding button nothing will happen

    private bool _isResetButtonHeldDown;
    private bool _isSmall;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.OnButtonReleased += Button_OnOnButtonReleased;
        button.OnButtonDown += Button_OnButtonDown;

        startingTransform.startingScale = tamagotchi.localScale;
        startingTransform.startingPosition = tamagotchi.position;
    }

    private void Button_OnButtonDown()
    {
        Debug.Log("Down");
        _isResetButtonHeldDown = true;
    }

    private void Update()
    {
        if (_isResetButtonHeldDown)
        {
            UpdateResetTimer();
        }
    }

    private void UpdateResetTimer()
    {
        resetTimer += Time.deltaTime;
        if (resetTimer > afterHowLongWillGameReset)
        {
            SaveManager.Instance.DeleteSaveData();
        }
    }

    private void Button_OnOnButtonReleased()
    {
        _isResetButtonHeldDown = false;
        if(resetTimer > afterHowLongGameWillShrink) ToggleSize();
        if (resetTimer > afterHowLongWillGameReset)
        {
            resetTimer = 0;
            return;
        }
#if UNITY_EDITOR
        if(resetTimer < afterHowLongGameWillShrink) UnityEditor.EditorApplication.isPlaying = false;
#else         
        if(resetTimer < afterHowLongGameWillShrink) Application.Quit();
#endif
        resetTimer = 0;
        SaveManager.Instance.SaveGame();
    }

    private void ToggleSize()
    {
        if (_isSmall)
        {
            tamagotchi.localScale = startingTransform.startingScale;
            tamagotchi.position = startingTransform.startingPosition;
            _isSmall = false;
        }
        else
        {
            tamagotchi.localScale = minimizedTamagotchiPosition.startingScale;
            tamagotchi.position = minimizedTamagotchiPosition.startingPosition;
            _isSmall = true;
        }
    }
    

    [Serializable]
    protected class StartingTransform
    {
        public Vector3 startingPosition;
        public Vector3 startingScale;
    }
}
