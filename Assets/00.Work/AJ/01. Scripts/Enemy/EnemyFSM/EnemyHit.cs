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
        _enemy.HealthCompo.OnHealthChanged += (float health, float maxHealth) => _enemy.isHit = true;
        _enemy.HealthCompo.OnDead += () => _enemy.isDead = true;
        _enemyAnimator.OnDeathEndTrigger += () => Destroy(_enemy.gameObject);
    }
}
