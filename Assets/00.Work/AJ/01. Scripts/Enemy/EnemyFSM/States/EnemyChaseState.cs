using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class EnemyChaseState : EnemyState
{
    private EnemyAttack _enemyAttack;
    public EnemyChaseState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _enemyAttack = enemy.GetComponent<EnemyAttack>();
    }
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Chase State");
        _enemy.ChangeFlip(true);
        _enemy.AgentCompo.speed = _enemy.enemySO.speed;
        _enemy.AgentCompo.stoppingDistance = _enemy.AttackRange;
        _enemy.AgentCompo.isStopped = false;
    }
    public override void Update()
    {
        base.Update();
        if (_enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            _stateMachine.ChangeState(EnemyStateType.Attack);
        }
        _enemy.AgentCompo.SetDestination(_enemy.target.position);

        if (!_enemy.CheckChaseRange())
        {
            _stateMachine.ChangeState(EnemyStateType.Idle);
        }
        else if (_enemy.CheckAttackRange())
        {
            _enemy.AgentCompo.isStopped = true;
            _enemy.AgentCompo.autoBraking = false;
            _stateMachine.ChangeState(EnemyStateType.Attack);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
