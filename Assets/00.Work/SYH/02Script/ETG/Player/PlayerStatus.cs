using UnityEngine;

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
    public float _mana;
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
    private void Awake()
    {
        _hp = _fullHp;
        _mana = _fullMana;
    }
}
