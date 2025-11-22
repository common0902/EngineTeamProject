using _00.Work.AJ._01._Scripts.Boss2.Attacks;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MBossCircleRangeState : MiddleBossState
    {
        private MiddleBossAttack _bossAttack;
        public MBossCircleRangeState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<MiddleBossAttack>();
        }
        public override void Enter()
        {
            base.Enter();
            
        }
        public override void Update()
        {
            base.Update();
            _boss.RbCompo.linearVelocity = Vector2.zero;
            if (_bossAttack.IsAnimationEnd)
            {
                _bossAttack.IsAnimationEnd = false;
                _stateMachine.ChangeState(MiddleBossStateType.Idle);
                return;
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}