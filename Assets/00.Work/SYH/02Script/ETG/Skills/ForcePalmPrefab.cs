using DG.Tweening;
using UnityEngine;

public class ForcePalmPrefab : SkillPrefab
{
    Vector2 _dir;
    private void Start()
    {
        _dir = SkillAimming.AimWeapon(transform);
        //transform.position += (Vector3)_dir * _duration;
    }
    protected override void Update()
    {
        base.Update();
        transform.DOMove(transform.position + (Vector3)_dir, 1).SetEase(Ease.OutQuart);
    }
}
