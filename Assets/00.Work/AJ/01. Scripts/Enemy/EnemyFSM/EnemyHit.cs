using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    public bool isAnimationEnd = false;
    private Enemy _enemy;
    private void Awake()
    {
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        _enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        _enemyAnimator.OnHitEndTrigger += () => isAnimationEnd = true;
        _enemy.HealthCompo.OnDamage += () => _enemy.isHit = true;
        _enemy.HealthCompo.OnDead += () => Destroy(gameObject);
    }
}
