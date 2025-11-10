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
    
    // Enemy가 IsPlayerInSight 만약에 Enemy의 오른쪽에 벽이 있고 플레이어는 그 벽 뒤에 있다! 그러면 플레이어의 오른쪽으로 이동
    // Enemy의 IsPlayerInSight 만약에 Enemy의 왼쪽에 벽이 있고 플레이어는 그 벽 뒤에 있다! 그러면 플레이어의 왼쪽을 타겟으로 잡아서 이동
    // 
    
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

        
        _enemy.AgentCompo.SetDestination(_enemy.target.position);
        
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