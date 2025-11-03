using UnityEngine;

public abstract class SkillPrefab : MonoBehaviour
{
    public float _shotSpeed;
    public float _damage;
    public float _duration;
    float _waitTime;
    protected virtual void Update()
    {
        if (_duration == 0) return;
        if (_waitTime >= _duration)
        {
            PoolManager.Instance.Push(GetComponent<IPoolable>());
            _waitTime = 0;
            gameObject.SetActive(false);
        }
        _waitTime += Time.deltaTime;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        _waitTime = 0;
        if (collision.gameObject.TryGetComponent(out HealthSystem hp))
        {
            hp.Damage(SkillUtility.CalcurateDamage(_damage));
        }
    }
}
