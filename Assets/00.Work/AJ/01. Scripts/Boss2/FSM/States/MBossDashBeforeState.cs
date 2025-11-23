using _00.Work.AJ._01._Scripts.Boss2.Attacks;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MBossDashBeforeState : MiddleBossState
    {
        private MiddleBossAttack _bossAttack;
        private float _timer;
        private float _waitTime = 2f;
        public MBossDashBeforeState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<MiddleBossAttack>();
        }
        public override void Enter()
        {
            base.Enter();
            _timer = 0f;
        }
        public override void Update()
        {
            base.Update();
            _timer += Time.deltaTime;
            _boss.HealthCompo.Invincibility = true;
            if (_bossAttack.IsAnimationEnd)
            {
                if (_timer > _waitTime)
                {
                    _bossAttack.IsAnimationEnd = false;
                    _stateMachine.ChangeState(MiddleBossStateType.Dash);
                    _timer = 0f;
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