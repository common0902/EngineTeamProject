using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM
{
    public abstract class BossState
    {
        protected Boss _boss;
        protected BossStateMachine _stateMachine;
        protected int _animHash;
        public BossState(Boss boss, string animName, BossStateMachine stateMachine)
        {
            _boss = boss;
            _stateMachine = stateMachine;
            _animHash = Animator.StringToHash(animName);
        }
        
        public virtual void Enter()
        {
            _boss.AnimCompo.SetBool(_animHash, true);
        }
        public virtual void Update()
        {
            if (_boss.IsDead)
            {
                _stateMachine.ChangeState(BossStateType.Dead);
            }
            if (_boss.IsHit)
            {
                _stateMachine.ChangeState(BossStateType.Hit);
            }
        }
        public virtual void Exit()
        {
            _boss.AnimCompo.SetBool(_animHash, false);
        }
    }
}