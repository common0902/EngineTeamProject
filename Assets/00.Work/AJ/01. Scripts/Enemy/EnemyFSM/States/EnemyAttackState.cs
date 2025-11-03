using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private EnemyAttack _enemyAttack;
    private bool isAttack;
    public EnemyAttackState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _enemyAttack = enemy.GetComponent<EnemyAttack>();
    }
    public override void Enter()
    {
        base.Enter();
        _enemyAttack.isAnimationEnd = false;
        _enemy.ChangeFlip(false);
        if (_enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            var sr = _enemy.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
                sr.color = new Color(1, 1, 1, 0); 
            _enemy.HealthCompo.enabled = false;
            _enemy.ColliderCompo.isTrigger = true;
        }
    }
    public override void Update()
    {
        base.Update();
        if (_enemy.CheckAttackRange())
        {
            CalculateTargetRotation();
        }
        if (_enemy.CheckAttackRange() && !isAttack)
        {
            isAttack = true;
            _enemyAttack.Attack();
        }

        if (_enemyAttack.isAnimationEnd)
        {
            if (_enemy.enemySO.enemyType == EnemyType.Assassin)
            {
                _stateMachine.ChangeState(EnemyStateType.Idle);
            }
            else if (_enemy.enemySO.enemyType == EnemyType.SuisideAttacker)
            {
                
            }
            else
            {
                _stateMachine.ChangeState(EnemyStateType.Chase);
            }
        }
    }

    private void CalculateTargetRotation()
    {
        Vector2 direction = (_enemy.target.position - _enemy.transform.position).normalized;
             
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -45f, 45f); 

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        _enemy.transform.rotation = targetRotation;
        _enemy.VisualCompo.Flip(direction);
    }
}
