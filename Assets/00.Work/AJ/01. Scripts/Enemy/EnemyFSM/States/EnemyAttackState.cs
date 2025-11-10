using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyAttackState : EnemyState
{
    private EnemyAttack _enemyAttack;
    private bool isAttack;

    private float delay = 0;//
    private float timer = 0;//
    public EnemyAttackState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
    {
        _enemyAttack = enemy.GetComponent<EnemyAttack>();

        delay = enemy.enemySO.attackDelay;//
    }
    public override void Enter()
    {
        base.Enter();
        _enemyAttack.isAnimationEnd = false;
        _enemy.ChangeFlip(false);
        _enemy.VisualCompo.Flip(_enemy.target.position - _enemy.transform.position);
        if (_enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            var sr = _enemy.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
                sr.color = new Color(1, 1, 1, 0); 
            _enemy.HealthCompo.enabled = false;
            _enemy.ColliderCompo.isTrigger = true;
            _enemy.vfx.Play();
        }
        
    }
    public override void Update()
    {
        base.Update();
        if (_enemy.CheckAttackRange())
        {
            CalculateTargetRotation();
        }
        /*if (_enemy.enemySO.useBoxRange)
        {
            if (_enemy.CheckAttackRangeBox() && !isAttack) 
            {
                isAttack = true;
            }
        }
        else if (_enemy.CheckAttackRange() && !isAttack)
        {
            isAttack = true;
        }*/

        if (_enemyAttack.isAnimationEnd)
        {
            if (_enemy.enemySO.enemyType == EnemyType.Assassin)
            {

            }
            else if (_enemy.enemySO.enemyType == EnemyType.SuisideAttacker)
            {

            }
            else if (_enemy.enemySO.enemyType == EnemyType.Ranged)
            {
                timer += Time.deltaTime;
                if (timer >= delay)
                {
                    _stateMachine.ChangeState(EnemyStateType.Idle);
                    timer = 0;
                }
            }
            else
            {
                _stateMachine.ChangeState(EnemyStateType.Idle);
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

    public override void Exit()
    {
        base.Exit();
    }
}
