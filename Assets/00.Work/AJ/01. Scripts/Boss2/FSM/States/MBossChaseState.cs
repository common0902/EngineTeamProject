using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MiddleBossChaseState : MiddleBossState
    {
        private float _speed;
        public MiddleBossChaseState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
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

            if (_boss.CheckAttackRange() && _boss.CurrentType == MiddleBossStateType.Range)
            {
                _stateMachine.ChangeState(MiddleBossStateType.Range);
                return;
            }
            if (_boss.CheckDamageRange() && _boss.CurrentType == MiddleBossStateType.Attack)
            {
                _stateMachine.ChangeState(MiddleBossStateType.Attack);
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