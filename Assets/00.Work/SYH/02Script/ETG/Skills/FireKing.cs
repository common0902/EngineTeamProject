using System.Collections;
using UnityEngine;

public class FireKing : Skill
{
    [SerializeField] float _fireKing;
    [SerializeField] float _transformationTime;
    float _fullHp;
    float _hp;
    protected override void UseSkill()
    {
        Player.Instance.PlayerAnimationCompo.SetAnimatorController(1);
        Player.Instance.PlayerStatusCompo._fireKing = _fireKing;
        _fullHp = Player.Instance.PlayerStatusCompo._fullHp;
        Player.Instance.PlayerStatusCompo._fullMana += _fullHp;
        _hp = Player.Instance.PlayerStatusCompo._hp;
        Player.Instance.PlayerStatusCompo._mana += _hp;
        Player.Instance.PlayerStatusCompo._skillCoolDownSpeed += 100;

        Player.Instance.PlayerStatusCompo._fullHp = 0;
        Player.Instance.PlayerStatusCompo._hp = 0;
        Player.Instance._isFireKing = true;
        StartCoroutine(TransformationTime());
    }
    IEnumerator TransformationTime()
    {
        yield return new WaitForSeconds(_transformationTime);
        Player.Instance.PlayerAnimationCompo.SetAnimatorController(0);
        Player.Instance.PlayerStatusCompo._fireKing = 1;
        Player.Instance.PlayerStatusCompo._mana -= _hp;
        Player.Instance.PlayerStatusCompo._fullMana -= _fullHp;
        Player.Instance.PlayerStatusCompo._skillCoolDownSpeed -= 100;


        Player.Instance.PlayerStatusCompo._fullHp = _fullHp;
        Player.Instance.PlayerStatusCompo._hp = _hp;
        Player.Instance._isFireKing = false;
    }
}
