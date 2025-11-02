using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [field: SerializeField] public float Health { get; private set; }
    [SerializeField] float _maxHearth;
    public event Action OnDamage;
    public event Action OnDead;

    private void Awake()
    {
        Health = _maxHearth;
    }

    public void Damage(float damage)
    {
        Health -= damage;
        Mathf.Clamp(Health, 0, _maxHearth);
        OnDamage?.Invoke();
        if (Health <= 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }
}
