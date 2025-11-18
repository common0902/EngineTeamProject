using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyChaseState : EnemyState
{
    private EnemyAttack _enemyAttack;
    private Vector3 _targetSidePosition;
    private bool _hasCalculatedSidePosition = false;
    
    public EnemyChaseState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _enemyAttack = enemy.GetComponent<EnemyAttack>();
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemy.ChangeFlip(true);
        _enemy.AgentCompo.speed = _enemy.enemySO.speed;

        if (!_enemy.enemySO.useBoxRange)
            _enemy.AgentCompo.stoppingDistance = _enemy.AttackRange;
        else
            _enemy.AgentCompo.stoppingDistance = 0f;

       
        _enemy.AgentCompo.isStopped = false;
        _hasCalculatedSidePosition = false;
    }
    
    public override void Update()
    {
        base.Update();
        
        if (_enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            _stateMachine.ChangeState(EnemyStateType.Attack);
            return;
        }

        if (!_enemy.CheckChaseRange())
        {
            _stateMachine.ChangeState(EnemyStateType.Idle);
            return;
        }
        
        _enemy.AgentCompo.SetDestination(_enemy.Target.position);
        
        if (_enemy.CheckAttackRange())
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