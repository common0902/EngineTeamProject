using UnityEngine;

public class Gatling : Skill
{
    bool _isActivated;
    //protected override void Start()
    //{
    //    Player.Instance.SkillControllerCompo.OnChangeSkill += () =>
    //    {
    //        base.DisActive();
    //        CoolTime = 0.5f;
    //    };
    //}
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
        if (!_isActivated)
        {
            _isActivated = true;
            Player.Instance.PlayerStatusCompo._speed -= 3;
        }
    }
    public override void DisActive()
    {
        base.DisActive();
        if (_isActivated)
        {
            _isActivated = false;
            Player.Instance.PlayerStatusCompo._speed += 3;
            CoolTime = 0.5f;
        }
    }
}
