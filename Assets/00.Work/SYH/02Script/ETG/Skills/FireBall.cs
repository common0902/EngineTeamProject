using UnityEngine;

public class FireBall : Skill
{
    protected override void UseSkill()
    {
        base.UseSkill();
        GameObject fireBall = PoolManager.Instance.Pop(Name).GameObject;
        //FireBallPrefab fireBall = Instantiate(SkillPrefab, transform.position, Quaternion.identity).GetComponent<FireBallPrefab>();
        fireBall.transform.position = Player.Instance.FirePos.position;
    }
}
