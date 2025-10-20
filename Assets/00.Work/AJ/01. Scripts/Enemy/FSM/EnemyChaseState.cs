using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Chase State");
    }
    public override void Update()
    {
        //_enemy.AgentCompo.SetDestination(_enemy.target.position);
        if (!_enemy.CheckChaseRange())
        {
            _stateMachine.ChangeState(EnemyStateType.Idle);
        }
        else if (_enemy.CheckAttackRange())
        {
            _stateMachine.ChangeState(EnemyStateType.Attack);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
