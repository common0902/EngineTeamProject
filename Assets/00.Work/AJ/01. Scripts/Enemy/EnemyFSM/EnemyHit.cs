using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHit : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    public bool isAnimationEnd = false;
    private Enemy _enemy;
    private NormalRoom _parentRoom;
    public GoldSystem goldSystem;

    private void Awake()
    {
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        _enemy = GetComponent<Enemy>();
        _parentRoom = GetComponentInParent<NormalRoom>();
    }
    private void Start()
    {
        goldSystem = _enemy.Target.GetComponent<GoldSystem>();
        _enemyAnimator.OnHitEndTrigger += () => isAnimationEnd = true;
        _enemy.HealthCompo.OnHealthChanged += (float health, float maxHealth) => _enemy.IsHit = true;
        _enemy.HealthCompo.OnDead += () =>
        {
            _enemy.IsDead = true;
            _parentRoom.OnEnemyDied();
            goldSystem.SpawnGoldDrop(transform.position, Random.Range(3, 11));
        };
        _enemyAnimator.OnDeathEndTrigger += () => Destroy(_enemy.gameObject, 1f);
        _enemyAnimator.OnDeathTrigger += Death;
    }

    private void Death()
    {
        _enemy.HealthCompo.Heal(_enemy.enemySO.health);
    }
}
