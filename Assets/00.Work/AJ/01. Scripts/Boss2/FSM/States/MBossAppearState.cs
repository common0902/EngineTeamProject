using _00.Work.AJ._01._Scripts.Boss2.Attacks;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MBossAppearState : MiddleBossState
    {
        private MiddleBossAttack _bossAttack;
        private float _behindDistance = 1.5f;
        public MBossAppearState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<MiddleBossAttack>();
        }
        public override void Enter()
        {
            base.Enter();
            _boss.HealthCompo.Invincibility = false;
            _boss.transform.position = _boss.Target.position + new Vector3(_behindDistance, 0f, 0f);
            _boss.GetComponentInChildren<SpriteRenderer>().color = Color.white;
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