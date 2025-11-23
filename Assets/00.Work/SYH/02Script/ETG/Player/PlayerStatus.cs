using _00.Work.SYH._02Script.ETG;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    public float _sniper;
    public float _fireKing;
    public bool _isSaber;

    public float _damage;
    public event Action OnChangedHp;
    public float Damage
    {
        get
        {
            if (_damage < 1)
            {
                return 1;
            }
            else
            {
                if (_isSaber) return _damage * _skillCoolDownSpeed / 100f;
                return _damage * _sniper * _fireKing;
            }
        }
    }

    public float Mana
    {
        get => _mana;
        set
        {
            _mana = Mathf.Clamp(value, 0, _fullMana);
            CheckManaState();
        } 
    }
    
    public float _mana;
    public float _fullMana;
    public float _manaRecovry;
    public float _hp;
    [field: SerializeField]
    public float Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            OnChangedHp?.Invoke();
        }
    }
    public float _fullHp;
    public float _skillCoolDownSpeed;
    public float _speed;
    public float Speed
    {
        get
        {
            if (_speed < 1f)
            {
                return 1f;
            }
            else
            {
                return _speed;
            }
        }
        private set { _speed = value; }
    }

    private bool _isZeroMana = false;
    public UnityEvent OnZeroMana;
    public UnityEvent OnRecoverMana;
    HealthSystem _healthSystem;
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _hp = _fullHp;
        Mana = _fullMana;
        HpUpdate();
    }

    public void CheckManaState()
    {
        if (Mana <= 0.99f && !_isZeroMana)
        {
            _isZeroMana = true;
            OnZeroMana?.Invoke();
        }
        else if (Mana > 0.99f && _isZeroMana)
        {
            _isZeroMana = false;
            OnRecoverMana?.Invoke();
        }
    }

    public void HpUpdate()
    {
        _healthSystem.SetMaxHealth(_fullHp);
        _healthSystem.SetHealth(Hp);
    }
}
