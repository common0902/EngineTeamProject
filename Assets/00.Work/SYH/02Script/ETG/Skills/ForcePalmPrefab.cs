using DG.Tweening;
using UnityEngine;

public class ForcePalmPrefab : SkillPrefab
{
    Vector2 _dir;
    private void Start()
    {
        _dir = SkillUtility.AimWeapon(transform);
        transform.DOMove(transform.position + (Vector3)_dir, 1).SetEase(Ease.OutExpo);
    }
}
