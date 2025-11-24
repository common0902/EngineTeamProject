using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    private EnemyHit _enemyHit;
    private Vector2 _knockbackDir;
    private float _timer = 0;
    public EnemyHitState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _enemyHit = enemy.GetComponentInChildren<EnemyHit>();
    }
    
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Hit State");
        _enemy.ChangeFlip(false);

        if(SoundManager.Instance != null)
            SoundManager.Instance.PlaySound("Hit");
        if(_enemy.AgentCompo.enabled)
            _enemy.AgentCompo.isStopped = true;
        _knockbackDir = (_enemy.transform.position - _enemy.Target.position).normalized;
        //_enemy.RbCompo.AddForce(_knockbackDir * _enemy.enemySO.knockbackForce, ForceMode2D.Impulse);
        _enemy.transform.DOMove((Vector2)_enemy.transform.position + (_knockbackDir * _enemy.enemySO.knockbackForce), 0.1f);
        _timer = 0;
    }
    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _enemy.enemySO.knockBackTime)
        {
            _timer = 0f; 
            _enemy.RbCompo.linearVelocity = Vector2.zero;

            if (_enemyHit.isAnimationEnd)
            {
                _enemy.IsHit = false;
                _enemyHit.isAnimationEnd = false;
                _stateMachine.ChangeState(EnemyStateType.Idle);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
