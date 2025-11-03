using UnityEngine;

public class Gatling : Skill
{
    protected override void Start()
    {
        Player.Instance.SkillControllerCompo.OnChangeSkill += () =>
        {
            base.DisActive();
            CoolTime = 0.5f;
        };
    }
    protected override void UseSkill()
    {
        base.UseSkill();
        //FireBallPrefab fireBall = Instantiate(SkillPrefab, transform.position, Quaternion.identity).GetComponent<FireBallPrefab>();
        GatlingPrefab gatling = PoolManager.Instance.Pop(Name).GameObject.GetComponent<GatlingPrefab>();
        gatling.transform.position = Player.Instance.FirePos.position;
        if(CoolTime >= 0.05f) CoolTime -= CoolTime/20f;
    }
    public override void Active()
    {
        base.Active();
        Player.Instance.PlayerStatusCompo.Speed -= 3;
    }
    public override void DisActive()
    {
        base.DisActive();
        Player.Instance.PlayerStatusCompo.Speed += 3;
        CoolTime = 0.5f;
    }
}
