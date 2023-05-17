using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

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
    [SerializeField] private float sleepyIncreaseInterval = 1f;
    [Space]
    [SerializeField] private float love; //xp for all purposes
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

    public void FeedJelly()
    {
        if (currentHunger >= maxHunger)
        {
            RefuseFood();
            return;
        }
        currentHunger += feedValue;
        _gameManager.AddJellyDew(jellyDewAwardedForFeeding);
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
        yield return new WaitForSeconds(sleepyIncreaseInterval);
        currentSleepy += sleepIncreaseAmount;
        if (currentSleepy >= sleepThreshold)
        {
            StartSleeping();
            yield break;
        }
        StartCoroutine(BecomeSleepier());
    }

    private void StartSleeping()
    {
        IsJellyAsleep = true;
        _animator.PlaySleepAnim();
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
