using UnityEngine;

public class Marker : Skill
{
    protected override void UseSkill()
    {
        base.UseSkill();
        MarkerPrefab marker = PoolManager.Instance.Pop(Name).GameObject.GetComponent<MarkerPrefab>();
        if (marker.OnKillMarker == null)
        {
            marker.OnKillMarker += CooltimeReset;
        }
        marker.transform.position = Player.Instance.FirePos.position;
    }
    void CooltimeReset()
    {
        _waitTime = CoolTime;
    }
}
