using System.Collections;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private Vector2 _currentWayPoint;
    private bool _isWaitingForChase;
    public EnemyPatrolState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Patrol State");
        _enemy.ChangeFlip(false);
        
        _enemy.AgentCompo.stoppingDistance = 0f;
        _enemy.AgentCompo.isStopped = false;
        _enemy.AgentCompo.enabled = true;
        
        _isWaitingForChase = false;
        
        MoveToNextWaypoint();
    }
    public override void Update()
    {
        base.Update();
        
        _enemy.VisualCompo.Flip(_currentWayPoint - (Vector2)_enemy.transform.position);
        _enemy.AgentCompo.SetDestination(_currentWayPoint);
        
        if (_enemy.CheckChaseRange() && !_isWaitingForChase)
        {
            _isWaitingForChase = true;
            _enemy.StartCoroutine(WaitBeforeChase(Random.Range(0.1f, 0.5f)));
            return;
        }
        
        if (Vector3.Distance(_currentWayPoint, _enemy.transform.position) < _enemy.enemySO.patrolDist)
        {
            _enemy.AgentCompo.isStopped = true;
            _stateMachine.ChangeState(EnemyStateType.Idle);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void MoveToNextWaypoint()
    {
        try
        {
            _currentWayPoint = _enemy.wayPoints.GetNextWayPoint();
            _enemy.AgentCompo.SetDestination(_currentWayPoint);
        }
        catch(UnityException)
        {
            Debug.LogWarning($"{_enemy.name}에 WayPoints가 할당되지 않았습니다.");
            return;
        }
    }
    private IEnumerator WaitBeforeChase(float delay)
    {
        _enemy.AgentCompo.isStopped = true;
        yield return new WaitForSeconds(delay);
        _stateMachine.ChangeState(EnemyStateType.Chase);
    }

}
