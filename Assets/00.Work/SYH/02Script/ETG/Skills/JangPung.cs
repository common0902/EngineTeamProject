using UnityEngine;

public class JangPung : Skill
{
    protected override void UseSkill()
    {
        base.UseSkill();
        JangPungPrefab jangpung = PoolManager.Instance.Pop(Name).GameObject.GetComponent<JangPungPrefab>();
        //FireBallPrefab fireBall = Instantiate(SkillPrefab, transform.position, Quaternion.identity).GetComponent<FireBallPrefab>();
        jangpung.transform.position = Player.Instance.FirePos.position;
    }
}
