using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates
{
    public class BossAttackMeleeState : BossState
    {
        private BossAttack _bossAttack;
        public BossAttackMeleeState(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossAttack>();
        }

        public override void Enter()
        {
            base.Enter();
            _boss.RbCompo.linearVelocity = Vector2.zero;
            Debug.Log("Melee");
        }

        public override void Update()
        {
            base.Update();
            if (_bossAttack.IsAnimationEnd)
            {
                Debug.Log("End");
                _bossAttack.IsAnimationEnd = false;
                _stateMachine.ChangeState(BossStateType.ReturnToIdle);
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}