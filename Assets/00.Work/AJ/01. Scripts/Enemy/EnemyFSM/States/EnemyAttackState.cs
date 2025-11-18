using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyAttackState : EnemyState
{
    private EnemyAttack _enemyAttack;
    private bool isAttack;

    private float delay = 0;
    private float timer = 0;
    public EnemyAttackState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _enemyAttack = enemy.GetComponent<EnemyAttack>();

        delay = enemy.enemySO.attackDelay;
    }
    public override void Enter()
    {
        base.Enter();
        _enemyAttack.isAnimationEnd = false;
        if (_enemy.enemySO.enemyType != EnemyType.Ranged)
            _enemy.ChangeFlip(false);
        else
            _enemy.ChangeFlip(true);
        _enemy.VisualCompo.Flip(_enemy.Target.position - _enemy.transform.position);
    }

    public override void Update()
    {
        base.Update();
        if (_enemy.enemySO.enemyType == EnemyType.SuisideAttacker)
        {
            if (_enemyAttack.isAnimationEnd)
            {
                _stateMachine.ChangeState(EnemyStateType.Dead);
            }
            return; 
        }
        if (_enemyAttack.isAnimationEnd)
        {
            _enemyAttack.isAnimationEnd = false;

            switch (_enemy.enemySO.enemyType)
            {
                case EnemyType.Assassin:
                    if (_enemy.CheckChaseRange())
                        _stateMachine.ChangeState(EnemyStateType.Chase);
                    else
                        _stateMachine.ChangeState(EnemyStateType.Idle);
                    break;
                default:
                    _stateMachine.ChangeState(EnemyStateType.Idle);
                    break;
            }
        }
    }

    private void CalculateTargetRotation()
    {
        Vector2 direction = (_enemy.Target.position - _enemy.transform.position).normalized;
             
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -45f, 45f); 

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        _enemy.transform.rotation = targetRotation;
        _enemy.VisualCompo.Flip(direction);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
