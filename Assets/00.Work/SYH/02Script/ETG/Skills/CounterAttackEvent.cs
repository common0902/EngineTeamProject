using System;
using UnityEngine;

public class CounterAttackEvent : MonoBehaviour
{
    public event Action OnOverlab;
    public event Action OnEndCounter;
    public void OverlabTiming()
    {
        OnOverlab?.Invoke();
    }
    public void EndCounter()
    {
        OnEndCounter?.Invoke();
    }
}
