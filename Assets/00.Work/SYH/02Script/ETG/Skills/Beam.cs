using UnityEngine;

public class Beam : Skill
{
    GameObject _prefab;
    protected override void Awake()
    {
        base.Awake();
        _prefab = Instantiate(SkillPrefab, Player.Instance.FirePos.position, Quaternion.identity);
        _prefab.SetActive(false);
    }
    public override void Active()
    {
        base.Active();
        _prefab.SetActive(true);
    }
    public override void DisActive()
    {
        base.DisActive();
        _prefab.GetComponent<BeamPrefab>().EndAttack();
    }
    protected override void UseSkill()
    {
        if (!SkillUtility.CanUseSkill(Cost))
        {
            DisActive();
        }
        _prefab.GetComponent<BeamPrefab>().Attack();
    }
}
