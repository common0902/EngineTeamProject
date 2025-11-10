using System.Collections;
using TMPro;
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

        _enemy.AgentCompo.enabled = true;
        _enemy.AgentCompo.isStopped = true;
        
        var sr = _enemy.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            sr.color = new Color(1, 1, 1, 1);   
        if (_enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            var rend = _enemy.GetComponentInChildren<SpriteRenderer>();
            if (rend != null) rend.color = new Color(0, 0,0,0f);

            _enemy.HealthCompo.enabled = false;
            _enemy.ColliderCompo.isTrigger = true;
        }
        
        _waitDuration = Random.Range(0.5f, 1.5f);
        _waitTimer = 0f;
        
    }
    public override void Update()
    {
        base.Update();
        if (_enemy.IsOutScreen()) return;
        
        if (_enemy.enemySO.enemyType == EnemyType.Assassin && _enemy.CheckChaseRange())
        {
            _stateMachine.ChangeState(EnemyStateType.Attack);
            return;
        }
        
        if (_enemy.CheckChaseRange())
        {
            _stateMachine.ChangeState(EnemyStateType.Chase); 
            return;
        }

        if (_enemy.enemySO.canPatrol)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _waitDuration)
            {
                if (_enemy.wayPoints == null)
                    Debug.LogError("WayPoints is null!");
                else
                    _stateMachine.ChangeState(EnemyStateType.Patrol);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
