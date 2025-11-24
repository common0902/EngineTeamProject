using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Enemy))]
public class EnemyBrain : MonoBehaviour
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _stateMachine = new EnemyStateMachine();

        _stateMachine.AddState(EnemyStateType.Idle, new EnemyIdleState(_enemy, "Idle", _stateMachine));
        _stateMachine.AddState(EnemyStateType.Chase, new EnemyChaseState(_enemy, "Chase", _stateMachine));
        _stateMachine.AddState(EnemyStateType.Attack, new EnemyAttackState(_enemy, "Attack", _stateMachine));
        _stateMachine.AddState(EnemyStateType.Hit, new EnemyHitState(_enemy, "Hit", _stateMachine));
        _stateMachine.AddState(EnemyStateType.Patrol, new EnemyPatrolState(_enemy, "Chase", _stateMachine));
        _stateMachine.AddState(EnemyStateType.Dead, new EnemyDeathState(_enemy, "Death", _stateMachine));
    }
    private void Start()
    {
        _stateMachine.Initialized(EnemyStateType.Idle);
    }
    private void Update()
    {
        _stateMachine.CurrentState.Update();
    }
}
