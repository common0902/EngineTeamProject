using _00.Work.AJ._01._Scripts.BossClone;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneState
    {
        protected BossClone _boss;
        protected BossCloneStateMachine _stateMachine;
        protected int _animHash;
        public BossCloneState(BossClone boss, string animName, BossCloneStateMachine stateMachine)
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
                _stateMachine.ChangeState(BossCloneStateType.Death);
            }
            if (_boss.IsHit)
            {
                _stateMachine.ChangeState(BossCloneStateType.Hit);
            }
        }
        public virtual void Exit()
        {
            _boss.AnimCompo.SetBool(_animHash, false);
        }
    }
}