using _00.Work.AJ._01._Scripts.BossClone;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneDeathState : BossCloneState
    {
        private BossCloneHit _bossHit;
        public BossCloneDeathState(BossClone boss, string animName, BossCloneStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossHit = boss.GetComponent<BossCloneHit>();
        }
        public override void Enter()
        {
            base.Enter();
            
        }
        public override void Update()
        {
            if (_bossHit.isAnimationEnd)
            {
                Object.Destroy(_boss.gameObject);
            }
        }
        public override void Exit()
        {
            base.Exit();
            
        }
    }
}