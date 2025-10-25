using DG.Tweening;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
         private EnemyAttack _enemyAttack;
         public EnemyAttackState(Enemy enemy, string animName, EnemyStateMachine stateMachine) : base(enemy, animName, stateMachine)
         {
             _enemyAttack = enemy.GetComponent<EnemyAttack>();
         }
         public override void Enter()
         {
             base.Enter();
             _enemyAttack.isAnimationEnd = false;
             _enemy.ChangeFlip(false);
         }
         public override void Update()
         {
             base.Update();
             if (_enemyAttack.isAnimationEnd)
             {
                 _stateMachine.ChangeState(EnemyStateType.Chase);
             }
         }
         public override void Exit()
         {
             base.Exit();
         }
}
