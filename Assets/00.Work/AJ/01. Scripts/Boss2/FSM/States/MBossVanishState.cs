using _00.Work.AJ._01._Scripts.Boss2.Attacks;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MBossVanishState : MiddleBossState
    {
        private MiddleBossAttack _bossAttack;
        private float _timer;
        private float _waitTime = 2f;
        public MBossVanishState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<MiddleBossAttack>();
        }
        public override void Enter()
        {
            base.Enter();
            _boss.HealthCompo.Invincibility = true;
            _timer = 0f;
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_bossAttack.IsAnimationEnd)
            {
                if (_timer > _waitTime)
                {
                    _bossAttack.IsAnimationEnd = false;
                    _timer = 0f;
                    _stateMachine.ChangeState(MiddleBossStateType.Appear);
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