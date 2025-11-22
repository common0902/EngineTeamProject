using _00.Work.AJ._01._Scripts.BossClone;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneChaseState : BossCloneState
    {
        public BossCloneChaseState(BossClone boss, string animName, BossCloneStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Chase");
        }
        public override void Update()
        {
            base.Update();
            if (!_boss.CheckChaseRange())
            {
                _stateMachine.ChangeState(BossCloneStateType.Idle);
                return;
            }
            if (_boss.CheckAttackRange())
            {
                _stateMachine.ChangeState(BossCloneStateType.Attack);
                return;
            }
            Vector2 dir = (_boss.Target.position - _boss.transform.position).normalized;
            _boss.RbCompo.linearVelocity = dir * _boss.Speed;
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}