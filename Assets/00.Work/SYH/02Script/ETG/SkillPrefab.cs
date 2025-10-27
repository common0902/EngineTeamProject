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
            PoolManager.Instance.Push(GetComponent<IPoolable>());
            gameObject.SetActive(false);
        }
        _waitTime += Time.deltaTime;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem hp))
        {
            hp.Damage(SkillUtility.CalcurateDamage(_damage));
        }
    }
}
