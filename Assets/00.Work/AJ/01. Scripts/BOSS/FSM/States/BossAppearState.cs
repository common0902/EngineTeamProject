using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossAppearState : BossState
    {
        private float _appearDuration = 10f;
        private float _timer;
        
        public BossAppearState(Boss boss, string animName, BossStateMachine stateMachine) 
            : base(boss, animName, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _timer = 0f;
            _boss.RbCompo.linearVelocity = Vector2.zero;
            Debug.Log("Appear");
        }

        public override void Update()
        {
            base.Update();
            
            _timer += Time.deltaTime;
            
            if (_timer >= _appearDuration)
            {
                _stateMachine.ChangeState(BossStateType.AttackLaser);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}