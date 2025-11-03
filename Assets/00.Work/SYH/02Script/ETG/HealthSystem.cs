using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [field: SerializeField] public float Health { get; private set; }
    [SerializeField] private float _maxHealth;
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        private set
        {
               
        }
    }

    public event Action OnDamage;
    public event Action OnDead;

    private void Awake()
    {
        Health = _maxHealth;
    }
    [ContextMenu("Damage")]
    public void isDamage()
    {
        Damage(1);
    }
    public void Damage(float damage)
    {
        print(damage);
        Health -= damage;
        Mathf.Clamp(Health, 0, _maxHealth);
        OnDamage?.Invoke();
        if (Health < 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        OnDead?.Invoke();
    }
}
