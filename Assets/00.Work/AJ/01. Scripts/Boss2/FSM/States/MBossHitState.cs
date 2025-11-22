using _00.Work.AJ._01._Scripts.Boss2.Attacks;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MiddleBossHitState : MiddleBossState
    {
        private MiddleBossHit _bossHit;
        private Vector2 _knockbackDir;
        private float _timer;
        public MiddleBossHitState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossHit = boss.GetComponent<MiddleBossHit>();
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Hit");
        }

        public override void Update()
        {
            _boss.RbCompo.linearVelocity = Vector2.zero;
            if (_bossHit.isAnimationEnd)
            {
                _boss.IsHit = false;
                _bossHit.isAnimationEnd = false;
                _stateMachine.ChangeState(MiddleBossStateType.Idle);
            }
        }
    }
}