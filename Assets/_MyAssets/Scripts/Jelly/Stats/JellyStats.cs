using System;
using System.Collections;
using System.Drawing;
using _MyAssets.Scripts.Jelly;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;
using Random = UnityEngine.Random;

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
        Singing,
        Evolving
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

    [field: SerializeField]public JellyState CurrentJellyState { get; private set; }
    [SerializeField] private SpriteLibrary spriteLibrary;
    [SerializeField] private JellyEvolutionData evolutionData;
    [SerializeField] private JellyAge jellyAge;
    [Space] 
    [Tooltip("How often diferent triggers can happen in seconds value is multiplied for different events")]
    [SerializeField]private float timeIntervalForJellyTriggers = 10;
    [Space(25)]
    [SerializeField] private float maxHunger = 100;
    [SerializeField] private float currentHunger;
    [SerializeField] private float hungerDecreaseAmount = 1;
    [SerializeField] private float hungerInterval = 1f;
    [SerializeField] private float hungerThreshold = 50f;
    [SerializeField] private float nextTimeHungerSoundCanTriggered = 0; //how often hunger sound gets triggered
    [SerializeField] private SoundData hungryCallSound;
    [SerializeField] private SoundData refuseSound;
    [Space]
    [SerializeField] private int maxMood = 100;
    [SerializeField] private int currentMood = 100;
    [SerializeField] private int moodIncreaseValue = 5;
    [SerializeField] private JellyMood moodState;
    [SerializeField] private int moodRange = 20;
    [SerializeField] private int moodThreshold = 50;
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
    [Space (25)] 
    [SerializeField] private GrownJelly fullyGrownJellyPf;
    [SerializeField] private Transform grownJellyParent;
    
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
    /// Base stats for new eggs
    /// </summary>
    private void InitializeEggStats()
    {
        CurrentJellyState = JellyState.Idle;
        evolutionData = GetRandomEvolutionData();
        jellyAge = JellyAge.Egg;
        currentHunger = maxHunger;
        currentMood = maxMood;
        moodState = JellyMood.Happy;
        currentSleepy = 0;
        loveLevel = 0;
        love = 0;
        
        _animator.PlayIdleAnim();
    }

    private JellyEvolutionData GetRandomEvolutionData()
    {
        var evolutions = evolutionData.GetPossibleEvolutions();
        evolutionData = evolutions[Random.Range(0, evolutions.Length)];
        return evolutionData;
    }

    /// <summary>
    /// Jelly evolves from egg after X amount of time = love
    /// For now just increase love over time so it triggers natural evolution
    /// </summary>
    private void EggTimer()
    {
        IncreaseLove(Time.deltaTime);
    }

    private void CheckIfJellyCanEvolve()
    {
        if(_gameManager.CurrentlyActiveScene != ActiveScene.Main) return;

        if (love > loveNeededForEvolution && loveLevel == 3)
        {
            StartSpawningEgg();
            return;
        }
        
        if (jellyAge == JellyAge.Egg && love < loveNeededForEvolution)
        {
            EggTimer();
            return;
        }
        if (love >= loveNeededForEvolution)
        {
            LevelUpJelly();
            StartEvolving();
        }
    }

    //TODO: add animations/sounds 
    private void StartSpawningEgg()
    {
        _animator.PlayAdultWalkOffAnim();
    }

    /// <summary>
    /// Animation event when jelly is fully grown and goes of to background
    /// </summary>
    private void GrownJellyWalksAway()
    {
        InitializeEggStats();
        EvolveJelly(evolutionData.Egg);
        SpawnNewFullyEvolvedJellyOnBG();
    }

    private void SpawnNewFullyEvolvedJellyOnBG()
    {
        Instantiate(fullyGrownJellyPf, grownJellyParent);
    }

    private void LevelUpJelly()
    {
        love = 0;
        loveLevel++;
        _savedStats.LoveLevel = loveLevel;
        SaveManager.Instance.UpdateJellyStats(_savedStats);
    }

    private void StartEvolving()
    {
        //start playing evolve animation
        CurrentJellyState = JellyState.Evolving;
        _gameManager.LockButtons = true;
        _animator.PlayEvolutionAnim();
    }

    private void FinishEvolving()
    {
        _gameManager.LockButtons = false;
        _animator.PlayIdleAnim();
        CurrentJellyState = JellyState.Idle;
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
    
    /// <summary>
    /// Swaps how jelly looks to different sprite library asset
    /// </summary>
    /// <param name="spriteLibraryAsset"></param>
    private void EvolveJelly(SpriteLibraryAsset spriteLibraryAsset)
    {
        spriteLibrary.spriteLibraryAsset = spriteLibraryAsset;
        
        if(_savedStats == null) return;
        _savedStats.JellyAge = jellyAge;
        SaveManager.Instance.UpdateJellyStats(_savedStats);
    }

    /// <summary>
    /// During evolution animation swaps sprites between event1 and 2, to avoid making multiple animations for each evolution stage
    /// </summary>
    public void EvolutionAnimEvent1()
    {
        if(jellyAge == JellyAge.Egg) EvolveJelly(evolutionData.Egg);
        if(jellyAge == JellyAge.Baby) EvolveJelly(evolutionData.Baby);
        if(jellyAge == JellyAge.Young) EvolveJelly(evolutionData.Young);
        if(jellyAge == JellyAge.Adult) EvolveJelly(evolutionData.Adult);
    }
    
    public void EvolutionAnimEvent2()
    {
        if(jellyAge == JellyAge.Egg) EvolveJelly(evolutionData.Baby);
        if(jellyAge == JellyAge.Baby) EvolveJelly(evolutionData.Young);
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
            if (nextTimeHungerSoundCanTriggered < Time.realtimeSinceStartup)
            {
                nextTimeHungerSoundCanTriggered = Time.realtimeSinceStartup + UnityEngine.Random.Range(timeIntervalForJellyTriggers, timeIntervalForJellyTriggers * 2);
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
        love = Mathf.Clamp(love + amount, 0, loveNeededForEvolution);
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

    public JellyType GetJellyType()
    {
        return evolutionData.JellyType;
    }

    public int GetMoodLevel()
    {
        return currentMood;
    }

    public int GetMoodThreshold()
    {
        return moodThreshold;
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