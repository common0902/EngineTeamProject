using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    public float Health { get; private set; }
    public event Action<float> OnHealthChange;
    public event Action OnDead;
    public void GetDamage(float damage, GameObject target)
    {
        Health -= damage;
        OnHealthChange?.Invoke(Health);
        
        if (Health <= 0)
        {
            OnDead?.Invoke();
        }
    }
}
