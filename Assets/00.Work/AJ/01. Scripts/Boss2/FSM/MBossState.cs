using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM
{
    public class MiddleBossState
    {
        protected MiddleBoss _boss;
        protected MiddleBossStateMachine _stateMachine;
        protected int _animHash;
        public MiddleBossState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine)
        {
            _boss = boss;
            _stateMachine = stateMachine;
            _animHash = Animator.StringToHash(animName);
        }
        
        public virtual void Enter()
        {
            _boss.AnimCompo.SetBool(_animHash, true);
            /*var attack = _boss.GetComponent<BossAttack>();
            if (attack != null)
            {
                attack.IsAnimationEnd = false;
            }*/
        }
        public virtual void Update()
        {
            if (_boss.IsDead)
            {
                _stateMachine.ChangeState(MiddleBossStateType.Death);
            }
            if (_boss.IsHit)
            {
                _stateMachine.ChangeState(MiddleBossStateType.Hit);
            }
        }
        public virtual void Exit()
        {
            _boss.AnimCompo.SetBool(_animHash, false);
        }
    }
}