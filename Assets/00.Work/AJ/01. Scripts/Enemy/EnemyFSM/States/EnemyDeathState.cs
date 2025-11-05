using UnityEngine;

public class EnemyDeathState : EnemyState
{
    private HealthSystem _healthSystem;
    public EnemyDeathState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _healthSystem = enemy.GetComponent<HealthSystem>();
    }
    public override void Enter()
    {
        base.Enter();
        _enemy.AgentCompo.isStopped = true;
    }
    public override void Update()
    {
        if (_enemy.CheckDeathRange())
        {
            _enemy.target.GetComponent<HealthSystem>().Damage(_enemy.enemySO.deathDamage);
        }
    }
    public override void Exit() 
    {
        base.Exit();
    }
}
