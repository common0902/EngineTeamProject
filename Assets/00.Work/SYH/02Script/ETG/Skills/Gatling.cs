using UnityEngine;

public class Gatling : Skill
{
    protected override void UseSkill()
    {
        base.UseSkill();
        FireBallPrefab fireBall = Instantiate(SkillPrefab, transform.position, Quaternion.identity).GetComponent<FireBallPrefab>();
        fireBall.transform.position = Player.Instance.FirePos.position;
    }
}
