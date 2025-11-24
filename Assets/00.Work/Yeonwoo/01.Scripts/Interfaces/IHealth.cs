using System;

namespace _00.Work.Yeonwoo._01.Scripts.Interfaces
{
    public interface IHealth
    {
        float Health { get; }
        float MaxHealth { get; }
        event Action<float, float> OnHealthChanged;
        event Action OnLowHealth;
        event Action OnRecoverHealth;
        event Action OnDead;
    }
}