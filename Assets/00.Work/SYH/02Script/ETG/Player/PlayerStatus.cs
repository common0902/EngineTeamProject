using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    public float _sniper;
    public float _fireKing;

    public float _damage;
    
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
    
    private float _mana;
    public float _fullMana;
    public float _manaRecovry;
    public float _hp;
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
     
    private void Awake()
    {
        _hp = _fullHp;
        Mana = _fullMana;
    }

    public void CheckManaState()
    {
        if (Mana <= 0 && !_isZeroMana)
        {
            _isZeroMana = true;
            OnZeroMana?.Invoke();
        }
        else if (Mana > 0 && _isZeroMana)
        {
            _isZeroMana = false;
            OnRecoverMana?.Invoke();
        }
    }
}
