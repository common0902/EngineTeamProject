using _00.Work.AJ._01._Scripts.BossClone;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneIdleState : BossCloneState
    {
        private float _timer;
        private float _waitTime = 1.5f;
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
            
            _timer += Time.deltaTime;
            if (_timer >= _waitTime)
            {
                if (_boss.CheckChaseRange())
                {
                    _stateMachine.ChangeState(BossCloneStateType.Chase);
                    return;
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}