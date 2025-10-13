using System.Collections;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy _enemy;
    protected EnemyStateMachine _stateMachine;
    protected int _animHash;
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
        
    }
    public virtual void Exit()
    {
        _enemy.AnimCompo.SetBool(_animHash, false);
    }
}
