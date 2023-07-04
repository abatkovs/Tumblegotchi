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

    [Serializable]
    public enum JellyAge
    {
        Egg,
        Baby,
        Young,
        Adult,
    }

    [SerializeField] private GameObject code; //game code

    public JellyState CurrentJellyState { get; private set; }
    [SerializeField] private SpriteLibrary spriteLibrary;
    [SerializeField] private JellyEvolutionData evolutionData;
    [SerializeField] private JellyAge jellyAge;
    [Space] 
    [Tooltip("How often diferent triggers can happen in seconds")]
    [SerializeField]private float timeIntervalForJellyTriggers = 10;
    [Space(25)]
    [SerializeField] private float maxHunger = 100;
    [SerializeField] private float currentHunger;
    [SerializeField] private float hungerDecreaseAmount = 1;
    [SerializeField] private float hungerInterval = 1f;
    [SerializeField] private float hungerThreshold = 50f;
    [SerializeField] private SoundData hungryCallSound;
    [SerializeField] private SoundData refuseSound;
    [SerializeField] private float nextTimeHungerSoundCanTriggered = 0; //how often hunger sound gets triggered
    [Space]
    [SerializeField] private int maxMood = 100;
    [SerializeField] private int currentMood = 0;
    [SerializeField] private int moodIncreaseValue = 5;
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
    [SerializeField] private int loveLevel = 0;
    [SerializeField] private float love; //xp for all purposes
    [SerializeField] private float loveNeededForEvolution = 20;
    [SerializeField] private float feedingLoveIncrease = 1f;
    [Space]
    [SerializeField] private float feedValue = 5;
    [SerializeField] private int jellyDewAwardedForFeeding = 1;

    


    public bool IsJellyHungry { get; private set; }
    public bool IsJellyAsleep { get; private set; }
    
    private GameManager _gameManager;
    private SoundManager _soundManager;
    private JellyAnimator _animator;

    private SavedJellyStats _savedStats;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _soundManager = SoundManager.Instance;
        _animator = GetComponent<JellyAnimator>();
        currentHunger = maxHunger;
        _savedStats = new SavedJellyStats(jellyAge, currentHunger, currentMood, currentSleepy, love, loveLevel);
    }

    private void OnEnable()
    {
        Debug.Log("Enable stats");
        StartCoroutine(BecomeHungrier());
        StartCoroutine(BecomeSleepier());
    }

    private void Update()
    {
        CheckIfJellyCanEvolve();
    }

    /// <summary>
    /// Jelly evolves from egg after X amount of time = love
    /// For now just increase love over time so it triggers natural evolution
    /// </summary>
    private void EggTimer()
    {
        love += Time.deltaTime;
    }

    private void CheckIfJellyCanEvolve()
    {
        if(_gameManager.CurrentlyActiveScene != ActiveScene.Main) return;
        if (jellyAge == JellyAge.Egg && love < loveNeededForEvolution)
        {
            EggTimer();
            return;
        }
        if (love >= loveNeededForEvolution)
        {

            love = 0;
            loveLevel++;
            SaveManager.Instance.UpdateJellyStats(_savedStats);
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
        if (jellyAge == JellyAge.Egg)
        {
            EvolveJelly(evolutionData.Baby);
            jellyAge = JellyAge.Baby;
            return;
        }
        if (jellyAge == JellyAge.Baby)
        {
            EvolveJelly(evolutionData.Young);
            jellyAge = JellyAge.Young;
            return;
        }

        if (jellyAge == JellyAge.Young)
        {
            EvolveJelly(evolutionData.Adult);
            jellyAge = JellyAge.Adult;
            code.SetActive(true);
            return;
        }
    }

    private void EvolveJelly(SpriteLibraryAsset spriteLibraryAsset)
    {
        spriteLibrary.spriteLibraryAsset = spriteLibraryAsset;
        _savedStats.JellyAge = jellyAge;
        SaveManager.Instance.UpdateJellyStats(_savedStats);
    }

    public void EvolutionAnimEvent1()
    {
        if (jellyAge == JellyAge.Baby) spriteLibrary.spriteLibraryAsset = evolutionData.Baby;
        if(jellyAge == JellyAge.Young) EvolveJelly(evolutionData.Young);
        if(jellyAge == JellyAge.Adult) EvolveJelly(evolutionData.Adult);
    }
    
    public void EvolutionAnimEvent2()
    {
        if (jellyAge == JellyAge.Baby) EvolveJelly(evolutionData.Young);
        if(jellyAge == JellyAge.Young) EvolveJelly(evolutionData.Adult);
    }

    public bool IsJellyFull()
    {
        if (currentHunger >= maxHunger)
        {
            RefuseFood();
            SaveManager.Instance.UpdateJellyStats(_savedStats);
            return true;
        }
        return false;
    }
    
    public void FeedJelly()
    {
        currentHunger += feedValue;
        _savedStats.CurrentHunger = currentHunger;
        SaveManager.Instance.UpdateJellyStats(_savedStats);
        _gameManager.AddJellyDew(jellyDewAwardedForFeeding);
        IncreaseLove(feedingLoveIncrease);
        IncreaseMoodIfHungry();
    }

    private void IncreaseMoodIfHungry()
    {
        if (currentHunger < hungerThreshold)
        {
            ChangeMoodLevel(moodIncreaseValue);
        }
    }

    private void RefuseFood()
    {
        _soundManager.PlaySound(refuseSound);
    }

    private IEnumerator BecomeHungrier()
    {
        yield return new WaitForSeconds(hungerInterval);
        currentHunger = Mathf.Clamp(currentHunger - hungerDecreaseAmount, 0, maxHunger);
        _savedStats.CurrentHunger = currentHunger;
        SaveManager.Instance.UpdateJellyStats(_savedStats);
        StartCoroutine(BecomeHungrier());
        if (currentHunger < hungerThreshold)
        {
            if (nextTimeHungerSoundCanTriggered < Time.time)
            {
                nextTimeHungerSoundCanTriggered = Time.time + timeIntervalForJellyTriggers;
                _soundManager.PlaySound(hungryCallSound);
            }
            
            IsJellyHungry = true;
        }
        if (currentHunger > hungerThreshold) IsJellyHungry = false;
    }

    private IEnumerator BecomeSleepier()
    {
        yield return new WaitForSeconds(sleepIntervals);
        ChangeSleepyLevel(+sleepIncreaseAmount);
        _savedStats.CurrentSleepy = currentSleepy;
        SaveManager.Instance.UpdateJellyStats(_savedStats);
        if (currentSleepy >= sleepThreshold)
        {
            StartSleeping();
            yield break;
        }
        StartCoroutine(BecomeSleepier());
    }

    private void ChangeSleepyLevel(float value)
    {
        currentSleepy = Mathf.Clamp(currentSleepy + value, 0, sleepyMax);
    }

    private IEnumerator Sleeping()
    {
        yield return new WaitForSeconds(sleepIntervals);
        ChangeSleepyLevel(-sleepDecreaseAmount);
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

    public void ChangeEvolutionData(JellyEvolutionData newEvolutionData)
    {
        evolutionData = newEvolutionData;
        if (jellyAge == JellyAge.Egg)
        {
            spriteLibrary.spriteLibraryAsset = newEvolutionData.Egg;
        }
    }

    public void ChangeMoodLevel(int amount)
    {
        var moodLevel = 0;
        currentMood = Mathf.Clamp(currentMood + amount, 0, maxMood);

        _savedStats.CurrentMood = currentMood;
        SaveManager.Instance.UpdateJellyStats(_savedStats);

        moodLevel = currentMood / moodRange;

        moodState = (JellyMood)moodLevel;
    }

    public void IncreaseLove(float amount)
    {
        love += amount;
        _savedStats.Love = love;
        SaveManager.Instance.UpdateJellyStats(_savedStats);
    }
    
    public int GetLoveLevel()
    {
        return loveLevel;
    }

    public void LoadStats()
    {
        var saveData = SaveManager.Instance.SaveData;
        jellyAge = saveData.JellyAge;
        currentHunger = saveData.CurrentHunger;
        currentMood = saveData.CurrentMood;
        currentSleepy = saveData.CurrentSleepy;
        love = saveData.Love;
        loveLevel = saveData.LoveLevel;
        if(jellyAge == JellyAge.Baby) EvolveJelly(evolutionData.Baby);
        if(jellyAge == JellyAge.Young) EvolveJelly(evolutionData.Young);
        if(jellyAge == JellyAge.Adult) EvolveJelly(evolutionData.Adult);
    }

    public JellyAge GetJellyAge()
    {
        return jellyAge;
    }
}

public class SavedJellyStats
{
    public SavedJellyStats(JellyStats.JellyAge jellyAge, float currentHunger, int currentMood, float currentSleepy, float love, int loveLevel)
    {
        JellyAge = jellyAge;
        CurrentHunger = currentHunger;
        CurrentMood = currentMood;
        CurrentSleepy = currentSleepy;
        Love = love;
        LoveLevel = loveLevel;
    }
    
    public JellyStats.JellyAge JellyAge;
    public float CurrentHunger;
    public int CurrentMood;
    public float CurrentSleepy;
    public float Love;
    public int LoveLevel;
}