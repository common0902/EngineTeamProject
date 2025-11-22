using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossHitState : BossState
    {
        private BossHit _bossHit;
        private Vector2 _knockbackDir;
        private float _timer;
        public BossHitState(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossHit = boss.GetComponent<BossHit>();
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Hit");
        }

        public override void Update()
        {
            if (_bossHit.isAnimationEnd)
            {
                _boss.IsHit = false;
                _boss.HasStarted = false;
                _bossHit.isAnimationEnd = false;
                _stateMachine.ChangeState(BossStateType.Idle);
            }
        }
    }
}