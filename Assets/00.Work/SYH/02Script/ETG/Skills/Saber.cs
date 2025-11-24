using UnityEngine;

public class Saber : Skill
{
    public CounterAttack _skill;
    protected override void Awake()
    {
        _skill = GetComponent<CounterAttack>();
    } 
    public override void Passive()
    {
        Player.Instance.SkillControllerCompo.CurrentAutoAttackNum = 1;
        Player.Instance.PlayerStatusCompo._isSaber = true;
    }
    public override void DisPassive()
    {
        Player.Instance.SkillControllerCompo.CurrentAutoAttackNum = 0;
        Player.Instance.PlayerStatusCompo._isSaber = false;
    }
}

