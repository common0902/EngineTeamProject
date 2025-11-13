using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy _enemy;
    protected EnemyStateMachine _stateMachine;
    protected int _animHash;
    private EnemyHit _enemyHit;
    private Vector2 _knockbackDir;
    public EnemyState(Enemy enemy, string animName, EnemyStateMachine stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _animHash = Animator.StringToHash(animName);
    }
    public virtual void Enter()
    {
        _enemy.AnimCompo.SetBool(_animHash, true);
    }
    public virtual void Update()
    {
        if (_enemy.IsDead)
        {
            _stateMachine.ChangeState(EnemyStateType.Dead);
        }
        if (_enemy.IsHit)
        {
            _stateMachine.ChangeState(EnemyStateType.Hit);
        }
    }
    public virtual void Exit()
    {
        _enemy.AnimCompo.SetBool(_animHash, false);
    }
}
