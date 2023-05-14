using UnityEngine;

public class JellyStats : MonoBehaviour
{
    [SerializeField] private float hunger;
    [SerializeField] private float mood;
    [SerializeField] private float sleepy;
    [SerializeField] private float love; //xp for all purposes
    [Space]
    [SerializeField] private float feedValue = 5;
    [SerializeField] private int jellyDewAwardedForFeeding = 1;
    
    public void FeedJelly()
    {
        hunger += feedValue;
        GameManager.Instance.AddJellyDew(jellyDewAwardedForFeeding);
    }
}
