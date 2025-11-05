using System;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using UnityEngine;

namespace _00.Work.SYH._02Script.ETG
{
    public class HealthSystem : MonoBehaviour, IHealth
    {
        [field: SerializeField] public float Health { get; private set; }
        [SerializeField] private float maxHealth;

        private bool _isLowHealth = false;
        
        public float MaxHealth => maxHealth;

        public event Action<float, float> OnHealthChanged;
        public event Action OnLowHealth;
        public event Action OnRecoverHealth;
        public event Action OnDead;

        private void Awake()
        {
            _isLowHealth = false;
            Health = maxHealth;
            OnHealthChanged?.Invoke(Health, maxHealth);
        }

        public void Damage(float damage)
        {
            if (damage <= 0) return;
            
            Health = Mathf.Clamp(Health - damage, 0, maxHealth);
            CheckHealthState();
        }
    
        public void Heal(float amount)
        {
            if (amount <= 0) return;

            Health = Mathf.Clamp(Health + amount, 0, maxHealth);
            CheckHealthState();
        }

        private void CheckHealthState()
        {
            OnHealthChanged?.Invoke(Health, maxHealth);

            if (Health <= 0)
            {
                Dead();
                return;
            }
        
            if (Health <= maxHealth / 5f && !_isLowHealth) // 20%를 의미
            {
                _isLowHealth = true;
                LowHealth();
            }
            else if (Health > maxHealth / 5f && _isLowHealth)
            {
                _isLowHealth = false;
                RecoverHealth();
            }
        }
    
        private void RecoverHealth()
        {
            OnRecoverHealth?.Invoke();
            Debug.Log("체력정상궤도");
        }

        private void LowHealth()
        {
            OnLowHealth?.Invoke();
            Debug.Log("체력부족");
        }

        private void Dead()
        {
            OnDead?.Invoke();
            Debug.Log("사망");
            //Destroy(gameObject);
        }
    }
}
