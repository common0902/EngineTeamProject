using UnityEngine;

public class ForcePalm : Skill
{
    public override void Active()
    {
        base.Active();
        ForcePalmPrefab forcePalm = Instantiate(SkillPrefab, transform.position, Quaternion.identity).GetComponent<ForcePalmPrefab>();
        forcePalm.transform.position = Player.Instance.transform.position;
    }
}
