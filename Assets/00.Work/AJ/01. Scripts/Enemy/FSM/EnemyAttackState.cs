using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Attack State");
    }
    public override void Update()
    {
        if (_enemy.CheckChaseRange())
        {
            _stateMachine.ChangeState(EnemyStateType.Chase);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
