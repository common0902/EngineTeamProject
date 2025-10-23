using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float _checkTimer = 0.3f;
    private float _lastCheckTime;
    public EnemyIdleState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Idle State");
        _enemy.AgentCompo.updateRotation = false;
        _enemy.AgentCompo.updateUpAxis = false;
        _enemy.AgentCompo.isStopped = true;
        _lastCheckTime = Time.time;
    }
    public override void Update()
    {
        if (_lastCheckTime + _checkTimer < Time.time)
        {
            if (_enemy.CheckChaseRange())
            {
                _stateMachine.ChangeState(EnemyStateType.Chase); 
                return;
            }
            _lastCheckTime = Time.time;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
