using _00.Work.AJ._01._Scripts.BossClone;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneAttackState : BossCloneState
    {
        public BossCloneAttack _bossAttack;
        public BossCloneAttackState(BossClone boss, string animName, BossCloneStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossCloneAttack>();
        }
        public override void Enter()
        {
            base.Enter();
            
        }
        public override void Update()
        {
            base.Update();
            _boss.RbCompo.linearVelocity = Vector2.zero;
            if (_bossAttack.isAnimationEnd)
            {
                _bossAttack.isAnimationEnd = false;
                _stateMachine.ChangeState(BossCloneStateType.Chase);
                return;
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}