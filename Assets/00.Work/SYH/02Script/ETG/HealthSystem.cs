using System;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using _00.Work.Yeonwoo._01.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.SYH._02Script.ETG
{
    public class HealthSystem : MonoBehaviour, IHealth
    {
        [field: SerializeField] public float Health { get; private set; }
        [SerializeField] private float maxHealth; // 최대체력 늘리기 효과 만들려면 이거 건드리면 됨

        private bool _isLowHealth = false;
        
        public float MaxHealth => maxHealth;

        public event Action<float, float> OnHealthChanged;
        public event Action OnLowHealth;
        public event Action OnRecoverHealth;
        public event Action OnDead;

        public bool _isCounter;
        public event Action OnCounter;

        private void Awake()
        {
            _isLowHealth = false;
        }

        private void Start()
        {
            //maxHealth = Player.Instance.PlayerStatusCompo._fullHp;
            Health = maxHealth;
            OnHealthChanged?.Invoke(Health, maxHealth);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Damage(1);
            }
        }
        public void Damage(float damage)
        {
            if(_isCounter)
            {
                OnCounter?.Invoke();
                return;
            }
            if (damage <= 0) return;
            Debug.Log("Damage");
            Health = Mathf.Clamp(Health - damage, 0, maxHealth);
            CreateDamageText(transform.position + Vector3.up * 1.5f, damage);
            print(222);
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
        
        public void SetMaxHealth(float health)
        {
            maxHealth = health;
        }
        private void Dead()
        {
            OnDead?.Invoke();
            Debug.Log("사망");
            //Destroy(gameObject);
        }

        private void CreateDamageText(Vector3 pos, float damage)
        {
            IPoolable poolable = PoolManager.Instance.Pop("DamageText");
            if (poolable is DamageTextUI damageText)
            {
                damageText.SetDamage(damage, pos);
            }
        }
    }
}
