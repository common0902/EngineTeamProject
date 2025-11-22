using System.Collections;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossChaseState : BossState
    {
        private float _speed;
        
        public BossChaseState(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Chase");
            _speed = _boss.Speed;
        }
        public override void Update()
        {
            base.Update();
            
            if (_boss.CheckAttackRange())
            {
                _stateMachine.ChangeState(_boss.CurrentType);
                return;
            }
            
            Vector2 direction = (_boss.Target.position - _boss.transform.position).normalized;
            _boss.RbCompo.linearVelocity = direction * _speed;
        }
        public override void Exit()
        {
            base.Exit();
            _boss.RbCompo.linearVelocity = Vector2.zero;
        }
    }
}