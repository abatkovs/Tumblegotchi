using System;
using System.Collections;
using _MyAssets.Scripts.Jelly;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;

public class JellyStats : MonoBehaviour
{
    public enum JellyMood
    {
        Angry,
        Sad,
        Neutral,
        Happy,
        Happier,
    }

    public enum JellyState
    {
        Idle,
        Fed,
        Pet,
        Walking,
        Sleeping,
    }

    public enum JellyAge
    {
        Baby,
        Young,
        Adult,
    }

    public JellyState CurrentJellyState { get; private set; }
    [SerializeField] private SpriteLibrary spriteLibrary;
    [SerializeField] private JellyEvolutionData evolutionData;
    [SerializeField] private JellyAge jellyAge;
    [Space(25)]
    [SerializeField] private float maxHunger = 100;
    [SerializeField] private float currentHunger;
    [SerializeField] private float hungerDecreaseAmount = 1;
    [SerializeField] private float hungerInterval = 1f;
    [Space]
    [SerializeField] private int maxMood = 100;
    [SerializeField] private int currentMood = 0;
    [SerializeField] private JellyMood moodState;
    [SerializeField] private int moodRange = 20;
    [Space]
    [SerializeField] private float sleepyMax = 100;
    [SerializeField] private float sleepThreshold = 60; //goes to sleep at this value
    [SerializeField] private float currentSleepy = 0;
    [SerializeField] private float sleepIncreaseAmount = 1f;
    [SerializeField] private float sleepDecreaseAmount = 5f;
    [FormerlySerializedAs("sleepyIncreaseInterval")] [SerializeField] private float sleepIntervals = 1f;
    [Space]
    [SerializeField] private float love; //xp for all purposes
    [SerializeField] private float loveNeededForEvolution = 20;
    [SerializeField] private float feedingLoveIncrease = 1f;
    [Space]
    [SerializeField] private float feedValue = 5;
    [SerializeField] private int jellyDewAwardedForFeeding = 1;


    public bool IsJellyHungry { get; private set; }
    public bool IsJellyAsleep { get; private set; }
    
    private GameManager _gameManager;
    private JellyAnimator _animator;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<JellyAnimator>();
        currentHunger = maxHunger;

        StartCoroutine(BecomeHungrier());
        StartCoroutine(BecomeSleepier());
    }

    private void Update()
    {
        CheckIfJellyCanEvolve();
    }

    private void CheckIfJellyCanEvolve()
    {
        if(_gameManager.CurrentlyActiveScene != ActiveScene.Main) return;
        if (love >= loveNeededForEvolution)
        {
            love = 0;
            StartEvolving();
        }
    }

    private void StartEvolving()
    {
        //start playing evolve animation
        _gameManager.LockButtons = true;
        _animator.PlayEvolutionAnim();
    }

    private void FinishEvolving()
    {
        _gameManager.LockButtons = false;
        _animator.PlayIdleAnim();
        if (jellyAge == JellyAge.Baby)
        {
            SheetYoung();
            jellyAge = JellyAge.Young;
            return;
        }

        if (jellyAge == JellyAge.Young)
        {
            SheetAdult();
            jellyAge = JellyAge.Adult;
            return;
        }
    }

    private void SheetYoung()
    {
        spriteLibrary.spriteLibraryAsset = evolutionData.Young;
    }

    private void SheetAdult()
    {
        spriteLibrary.spriteLibraryAsset = evolutionData.Adult;
    }

    public void EvolutionAnimEvent1()
    {
        if (jellyAge == JellyAge.Baby) spriteLibrary.spriteLibraryAsset = evolutionData.Baby;
        if(jellyAge == JellyAge.Young) SheetYoung();
        if(jellyAge == JellyAge.Adult) SheetAdult();
        }
    
    public void EvolutionAnimEvent2()
    {
        if (jellyAge == JellyAge.Baby) SheetYoung();
        if(jellyAge == JellyAge.Young) SheetAdult();
    }
    
    public void FeedJelly()
    {
        if (currentHunger >= maxHunger)
        {
            RefuseFood();
            return;
        }
        currentHunger += feedValue;
        _gameManager.AddJellyDew(jellyDewAwardedForFeeding);
        IncreaseLove(feedingLoveIncrease);
    }

    private void RefuseFood()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator BecomeHungrier()
    {
        yield return new WaitForSeconds(hungerInterval);
        currentHunger -= hungerDecreaseAmount;
        StartCoroutine(BecomeHungrier());
        if (currentHunger < 20) IsJellyHungry = true;
        if (currentHunger > 30) IsJellyHungry = false;
    }

    private IEnumerator BecomeSleepier()
    {
        yield return new WaitForSeconds(sleepIntervals);
        currentSleepy += sleepIncreaseAmount;
        if (currentSleepy >= sleepThreshold)
        {
            StartSleeping();
            yield break;
        }
        StartCoroutine(BecomeSleepier());
    }

    private IEnumerator Sleeping()
    {
        yield return new WaitForSeconds(sleepIntervals);
        currentSleepy -= sleepDecreaseAmount;
        if (currentSleepy <= 0)
        {
            CurrentJellyState = JellyState.Idle;
            StartCoroutine(BecomeSleepier());
            yield break;
        }
        StartCoroutine(Sleeping());
    }

    private void StartSleeping()
    {
        if(_gameManager.CurrentlyActiveScene != ActiveScene.Main) return;
        IsJellyAsleep = true;
        _animator.PlaySleepAnim();
        CurrentJellyState = JellyState.Sleeping;
        StartCoroutine(Sleeping());
    }

    public void ChangeMoodLevel(int amount)
    {
        var moodLevel = 0;
        currentMood += amount;

        moodLevel = currentMood / moodRange;

        moodState = (JellyMood)moodLevel;
    }

    public void IncreaseLove(float amount)
    {
        love += amount;
    }
}
