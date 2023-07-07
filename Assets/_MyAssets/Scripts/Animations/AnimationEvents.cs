using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public event Action OnFinishSinging;

    public void FinishSinging()
    {
        OnFinishSinging?.Invoke();
    }
}
