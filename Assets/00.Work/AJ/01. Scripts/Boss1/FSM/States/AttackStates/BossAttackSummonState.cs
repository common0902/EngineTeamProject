using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates
{
    public class BossAttackSummonState : BossState
    {
        private BossAttack _bossAttack;
        
        public BossAttackSummonState(Boss boss, string animName, BossStateMachine stateMachine) 
            : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossAttack>();
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("Summon Attack");
        }

        public override void Update()
        {
            base.Update();
            _boss.RbCompo.linearVelocity = Vector2.zero;
            if (_bossAttack.IsAnimationEnd)
            {
                _bossAttack.IsAnimationEnd = false;
                _stateMachine.ChangeState(BossStateType.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}