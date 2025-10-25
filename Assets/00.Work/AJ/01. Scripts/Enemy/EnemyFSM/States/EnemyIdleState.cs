using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float _waitTimer;
    private float _waitDuration;

    private float _checkTimer = 0.3f;
    private float _lastCheckTime;
    public EnemyIdleState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Idle State");
        _enemy.ChangeFlip(false);
        
        
        _enemy.AgentCompo.isStopped = true;
        _enemy.AgentCompo.enabled = false;
        
        _waitDuration = Random.Range(0.5f, 1.5f);
        _waitTimer = 0f;
        
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

        if (_enemy.enemySO.canPatrol)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _waitDuration && !_enemy.IsOutScreen())
            {
                _stateMachine.ChangeState(EnemyStateType.Patrol);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        _enemy.AgentCompo.enabled = true;
    }
}
