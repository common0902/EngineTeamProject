using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates
{
    public class BossAttackDashState : BossState
    {
        private BossAttack _bossAttack;
        public BossAttackDashState(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossAttack>();
        }
        public override void Enter()
        {
            base.Enter();
            //Debug.Log("Dash");
            _boss.RbCompo.linearVelocity = Vector2.zero;
        }
        public override void Update()
        {
            base.Update();
            if (_bossAttack.IsAnimationEnd)
            {
                _stateMachine.ChangeState(BossStateType.ReturnToIdle);
            }
        }
        public override void Exit()
        {
            base.Exit();
            _boss.RbCompo.linearVelocity = Vector2.zero;
        }
    }
}