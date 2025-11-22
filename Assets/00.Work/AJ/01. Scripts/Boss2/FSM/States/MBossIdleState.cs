using _00.Work.AJ._01._Scripts.BOSS;
using _00.Work.AJ._01._Scripts.BOSS.FSM;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MiddleBossIdleState : MiddleBossState
    {
        private float _waitTime = 3f;
        private float _timer;
        public MiddleBossIdleState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _timer = 0f;
            _boss.RbCompo.linearVelocity = Vector2.zero;
            
            if (!_boss.HasStarted)
            {
                _waitTime = Random.Range(0f, 1f);
                _stateMachine.ChangeState(MiddleBossStateType.Chase);
                _boss.HasStarted = true;
            }
            else
            {
                _waitTime = Random.Range(1f, 2f); 
                Debug.Log($"WaitTime : {_waitTime}");
            }
        }

        public override void Update()
        {   
            base.Update();
            if (_boss.IsDead || _boss.Phase3Executed) return;
            
            _timer += Time.deltaTime;
            if (_timer >= _waitTime)
            {
                ChoosePattern();
                _timer = 0f;
            }
            else
            {
                return;
            }
        }

        private void ChoosePattern()
        {
            var pool = _boss.Patterns;
            MiddleBossStateType next = pool[Random.Range(0, pool.Count)];
            Debug.Log($"Next Pattern : {next}");
            if (next == MiddleBossStateType.Appear)
            {
                _boss.CurrentType = next;
                _stateMachine.ChangeState(MiddleBossStateType.Vanish);
                return;
            }
            else if (next == MiddleBossStateType.Attack || next == MiddleBossStateType.Range)
            {
                _boss.CurrentType = next;
                Debug.Log("djafkda");   
                _stateMachine.ChangeState(MiddleBossStateType.Chase);
                return;
            }
            else if (next == MiddleBossStateType.CircleRange)
            {
                _boss.CurrentType = next;
                _stateMachine.ChangeState(MiddleBossStateType.CircleRange);
                return;
            }
            else if (next == MiddleBossStateType.Dash)
            {
                _boss.CurrentType = next;
                _stateMachine.ChangeState(MiddleBossStateType.DashBefore);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _timer = 0f;
        }
    }
}