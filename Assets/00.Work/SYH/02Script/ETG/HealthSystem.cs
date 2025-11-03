using System;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using _00.Work.Yeonwoo._01.Scripts.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IHealth
{
    [field: SerializeField] public float Health { get; private set; }
    [SerializeField] private float maxHealth;
        
    public float MaxHealth => maxHealth;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDead;

    private void Awake()
    {
        Health = maxHealth;
        OnHealthChanged?.Invoke(Health, maxHealth);
    }

    public void Damage(float damage)
    {
        if (damage <= 0) return;
            
        Health = Mathf.Clamp(Health - damage, 0, maxHealth);
        OnHealthChanged?.Invoke(Health, maxHealth);

        if (Health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        OnDead?.Invoke();
        Debug.Log("사망");
        //Destroy(gameObject);
    }
}
