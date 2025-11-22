using System;
using UnityEditor.Searcher;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    public bool isAnimationEnd = false;
    private Enemy _enemy;
    private Room _parentRoom;

    private void Awake()
    {
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        _enemy = GetComponent<Enemy>();
        _parentRoom = GetComponentInParent<Room>();
    }
    private void Start()
    {
        _enemyAnimator.OnHitEndTrigger += () => isAnimationEnd = true;
        _enemy.HealthCompo.OnHealthChanged += (float health, float maxHealth) => _enemy.IsHit = true;
        _enemy.HealthCompo.OnDead += () =>
        {
            _enemy.IsDead = true;
            _parentRoom.OnEnemyDied();
        };
        _enemyAnimator.OnDeathEndTrigger += () => Destroy(_enemy.gameObject, 1f);
        _enemyAnimator.OnDeathTrigger += Death;
    }

    private void Death()
    {
        _enemy.HealthCompo.Heal(_enemy.enemySO.health);
    }
}
