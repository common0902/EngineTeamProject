using _00.Work.SYH._02Script.ETG;
using Unity.AppUI.UI;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    private HealthSystem _healthSystem;
    private bool _isDead = false;
    public EnemyDeathState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _healthSystem = enemy.GetComponent<HealthSystem>();
    }
    public override void Enter()
    {
        base.Enter();
        _enemy.ColliderCompo.enabled = false;
        _enemy.AgentCompo.enabled = false;
        _enemy.ChangeFlip(false);
        
        if (_enemy.enemySO.enemyType == EnemyType.SuisideAttacker) return;
        if (_enemy.CheckDeathRange() && !_isDead && _enemy.enemySO.deathRange > 0)
        {
            _isDead = true;
            Collider2D[] hits = Physics2D.OverlapCircleAll(_enemy.transform.position, _enemy.enemySO.deathRange);
            foreach (var hit in hits)
            {
                var playerHealth = hit.GetComponent<HealthSystem>();
                if (playerHealth != null)
                {
                    playerHealth.Damage(_enemy.enemySO.damage);
                }
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        _enemy.StopAllCoroutines();
    }
}
