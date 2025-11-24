using _00.Work.AJ._01._Scripts.BossClone;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneHitState : BossCloneState
    {
        private BossCloneHit _bossHit;
        public BossCloneHitState(BossClone boss, string animName, BossCloneStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossHit = boss.GetComponent<BossCloneHit>();
        }
        public override void Enter()
        {
            base.Enter();
            
        }
        public override void Update()
        {
            base.Update();
            _boss.RbCompo.linearVelocity = Vector2.zero;
            if (_bossHit.isAnimationEnd)
            {
                _bossHit.isAnimationEnd = false;
                _boss.IsHit = false;
                _stateMachine.ChangeState(BossCloneStateType.Idle);
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}