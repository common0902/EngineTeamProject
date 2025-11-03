using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float _damage;
    public float _mana;
    public float _fullMana;
    public float _manaRecovry;
    public float _hp;
    public float _fullHp;
    [SerializeField] private float _speed;
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
        set { _speed = value; }
     }
    public float _skillCoolDownSpeed;
}
