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
        
        _enemy.AgentCompo.isStopped = true;
        _enemyHit.isAnimationEnd = false;
        
        _knockbackDir = (_enemy.transform.position - _enemy.target.position).normalized;
        _enemy.RbCompo.AddForce(_knockbackDir * _enemy.enemySO.knockbackForce, ForceMode2D.Impulse);
        _timer = 0;
    }
    public override void Update()
    {
        base.Update();
        _timer += Time.deltaTime;
        if (_timer >= _enemy.enemySO.knockBackTime)
        {
            _enemy.RbCompo.linearVelocity = Vector2.zero;

            if (_enemyHit.isAnimationEnd)
            {
                _stateMachine.ChangeState(EnemyStateType.Chase);
                return;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.AgentCompo.isStopped = false;
    }
}
