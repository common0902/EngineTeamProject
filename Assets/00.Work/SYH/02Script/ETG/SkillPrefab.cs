using UnityEngine;

public abstract class SkillPrefab : MonoBehaviour
{
    public Transform _target;
    public Vector3 _targetPos;
    public float _shotSpeed;
    public float _damage;
    public float _duration;
    float _waitTime;
    protected virtual void Update()
    {
        if (_waitTime >= _duration)
        {
            Destroy(gameObject);
        }
        _waitTime += Time.deltaTime;
    }
}
