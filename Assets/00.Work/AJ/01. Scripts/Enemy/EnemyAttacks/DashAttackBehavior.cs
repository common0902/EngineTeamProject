using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class DashAttackBehavior : IEnemyAttackBehavior
{
    private Enemy _enemy;
    private HealthSystem _targetHealth;
    private WaitForSeconds _attackDelay;

    public bool IsAttackAnimationEnd { get; set; }

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _targetHealth = _enemy.target.GetComponent<HealthSystem>();
        _attackDelay = new WaitForSeconds(_enemy.enemySO.attackDelay);
    }

    public IEnumerator ExecuteAttack(Vector2 direction)
    {
        _enemy.AgentCompo.isStopped = true;
        _enemy.ColliderCompo.isTrigger = true;

        _enemy.RbCompo.linearVelocity = direction * _enemy.enemySO.dashData.dashForce;
        yield return new WaitForSeconds(_enemy.enemySO.dashData.dashAttackTime);

        if (_enemy.CheckAttackRange())
            _targetHealth.Damage(_enemy.enemySO.damage);

        _enemy.RbCompo.linearVelocity = Vector2.zero;
        yield return _attackDelay;

        _enemy.ColliderCompo.isTrigger = false;
        _enemy.AgentCompo.isStopped = false;
    }

    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }
}
