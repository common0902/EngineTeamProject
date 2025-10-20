using UnityEngine;

public class ForcePalm : Skill
{
    public override void Active()
    {
        base.Active();
        UtilityCorutineManager.Instance.StartPastDelay(this, PastDelay);
    }
    public override void EndPastDelay()
    {
        ForcePalmPrefab forcePalm = Instantiate(SkillPrefab, transform.position, Quaternion.identity).GetComponent<ForcePalmPrefab>();
        forcePalm.transform.position = Player.Instance.FirePos.position;
    }
}
