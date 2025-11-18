using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates
{
    public class BossAttackLaserState : BossState
    {
        private BossAttack _bossAttack;
        
        public BossAttackLaserState(Boss boss, string animName, BossStateMachine stateMachine) 
            : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossAttack>();
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("Laser Attack");
            _boss.RbCompo.linearVelocity = Vector2.zero;
            
            Vector2 direction = (_boss.Target.position - _boss.transform.position).normalized;
            _boss.VisualCompo.Flip(direction);
        }

        public override void Update()
        {
            base.Update();
            
            if (_bossAttack.IsAnimationEnd)
            {
                _bossAttack.IsAnimationEnd = false;
                _boss.transform.DOMove(_boss.CenterPos.position, 1f)
                    .OnComplete(() => _stateMachine.ChangeState(BossStateType.Idle));
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}