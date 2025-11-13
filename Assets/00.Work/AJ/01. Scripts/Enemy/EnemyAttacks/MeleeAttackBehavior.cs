using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class MeleeAttackBehavior : IEnemyAttackBehavior
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
        if (_enemy.CheckAttackRange())
        {
            yield return _attackDelay;
            
            _targetHealth.Damage(_enemy.enemySO.damage);
        }
    }

    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }
}
