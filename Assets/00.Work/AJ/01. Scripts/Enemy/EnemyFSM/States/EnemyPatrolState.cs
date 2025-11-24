using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyPatrolState : EnemyState
{
    private Vector2 _currentWayPoint;
    private bool _isWaitingForChase;
    private float _lastTrapTime;
    public EnemyPatrolState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _lastTrapTime = 0f;
    }
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Patrol State");
        _enemy.ChangeFlip(false);
        
        _enemy.AgentCompo.stoppingDistance = 0f;
        if(_enemy.AgentCompo.enabled)
            _enemy.AgentCompo.isStopped = false;
        
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
        if (_enemy.enemySO.enemyType == EnemyType.Trapper && !_enemy.IsOutScreen())
        {
            TryPlaceTrapWhilePatrolling();
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
            _currentWayPoint = _enemy.WayPoints.GetRandomWayPoint();
            _enemy.AgentCompo.SetDestination(_currentWayPoint);
        }
        catch(NullReferenceException)
        {
            Debug.LogWarning($"WayPoints is null");
            return;
        }
    }
    private void TryPlaceTrapWhilePatrolling()
    {
        if (_enemy.enemySO.trapperData == null) return;
        
        float currentTime = Time.time;
        float patrolTrapInterval = _enemy.enemySO.trapperData.trapPlaceInterval * 1.5f; 
        
        if (currentTime - _lastTrapTime < patrolTrapInterval)
            return;
        
        float distanceToWaypoint = Vector2.Distance(_enemy.transform.position, _currentWayPoint);
        if (distanceToWaypoint < 0.5f)
            return;
        
        _lastTrapTime = currentTime;
        PlaceTrap();
    }
    private void PlaceTrap()
    {
        EnemyAttack attackComponent = _enemy.GetComponent<EnemyAttack>();
        if (attackComponent != null)
        {
            _enemy.StartCoroutine(PlaceTrapDirectly());
        }
    }
    private IEnumerator PlaceTrapDirectly()
    {
        GameObject trapObj = Object.Instantiate(
            _enemy.enemySO.trapperData.trapPrefab,
            _enemy.transform.position,
            Quaternion.identity
        );

        Trap trap = trapObj.GetComponent<Trap>();
        if (trap != null)
        {
            trap.Initialize(_enemy.enemySO.trapperData);
        }

        if (_enemy.enemySO.trapperData.placeTrapVFX != null)
        {
            ParticleSystem vfx = Object.Instantiate(
                _enemy.enemySO.trapperData.placeTrapVFX,
                _enemy.transform.position,
                Quaternion.identity
            );
            vfx.Play();
            Object.Destroy(vfx.gameObject, 2f);
        }

        yield return null;
    }
    private IEnumerator WaitBeforeChase(float delay)
    {
        _enemy.AgentCompo.isStopped = true;
        yield return new WaitForSeconds(delay);
        _stateMachine.ChangeState(EnemyStateType.Chase);
    }

}
