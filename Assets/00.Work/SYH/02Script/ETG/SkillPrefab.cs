using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public abstract class SkillPrefab : MonoBehaviour
{
    public float _shotSpeed;
    public float _damage;
    public float _duration;
    float _waitTime;
    [SerializeField] AudioClip _sound;
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
        GameObject effect = PoolManager.Instance.Pop("HitEffect").GameObject;
        effect.transform.position = collision.ClosestPoint(transform.position);
    }
}
