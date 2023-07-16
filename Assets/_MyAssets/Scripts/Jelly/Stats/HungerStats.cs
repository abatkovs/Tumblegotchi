
//TODO: Finish refactoring
public class HungerStats
{
    /*private float maxHunger = 100;
    private float currentHunger;
    private float hungerDecreaseAmount = 1;
    private float hungerInterval = 1f;
    private float hungerThreshold = 50f;
    private float nextTimeHungerSoundCanTriggered = 0;*/ //how often hunger sound gets triggered
    
    /*public bool IsJellyFull()
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
    }*/
}
