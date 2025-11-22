using _00.Work.AJ._01._Scripts.BossClone;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneIdleState : BossCloneState
    {
        public BossCloneIdleState(BossClone boss, string animName, BossCloneStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            
        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            base.Update();
            if (_boss.CheckChaseRange())
            {
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