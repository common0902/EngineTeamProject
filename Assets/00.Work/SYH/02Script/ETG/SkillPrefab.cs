using UnityEngine;

public abstract class SkillPrefab : MonoBehaviour
{
    Transform _target;
    float _shotSpeed;
    float _damage;
    float _duration;


    SkillPrefab(Transform target, float shotSpeed, float damage, float duration)
    {
        _target = target;
        _shotSpeed = shotSpeed;
        _damage = damage;
        _duration = duration;
    }
}
