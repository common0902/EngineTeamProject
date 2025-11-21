using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossIdleState : BossState
    {
        private float _waitTime;
        private float _timer;

        public BossIdleState(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName,
            stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            if (!_boss.HasStarted && !_boss.Phase3Executed)
            {
                _waitTime = Random.Range(0f, 1f);
                _stateMachine.ChangeState(BossStateType.Chase);
                _boss.HasStarted = true;
            }
            else
            {
                _waitTime = Random.Range(2f, 3f); 
                Debug.Log($"WaitTime : {_waitTime}");
            }
        }

        public override void Update()
        {
            base.Update();
            if (_boss.IsDead || _boss.Phase3Executed) return;
            HandlePhase();
            
            _timer += Time.deltaTime;
            if (_timer > _waitTime)
            {
                Debug.Log("Hello");
                DecideNextPattern();
                _timer = 0f;
            }
            else
            {
                return;
            }
        }

        private void DecideNextPattern()
        {
            if (_boss.Phase3Executed)
            {
                return;
            }
            
            var pool = _boss.Patterns;
            BossStateType next = pool[Random.Range(0, pool.Count)];
            Debug.Log($"Next Pattern : {next}");
            
            if (next == BossStateType.AttackBomb)
            {
                _boss.CurrentType = next;
                _stateMachine.ChangeState(BossStateType.AttackBomb);
                return;
            }
            if (next == BossStateType.AttackLaser)
            {
                _boss.CurrentType = next;
                _stateMachine.ChangeState(BossStateType.Vanish);
                return;
            }
        
            if (next == BossStateType.AttackMelee || next == BossStateType.AttackDash)
            {
                _boss.CurrentType = next;
                _stateMachine.ChangeState(BossStateType.Chase);
                return;
            }

            if (next == BossStateType.AttackRange || next == BossStateType.AttackSummon)
            {
                _boss.CurrentType = next;
                _stateMachine.ChangeState(next);
                return;
            }
        }

        private void HandlePhase()
        {
            float hpRate = (_boss.HealthCompo.Health / _boss.HealthCompo.MaxHealth) * 100f;

            if (!_boss.Phase2Unlocked && hpRate <= 60f)
            {
                _boss.Phase2Unlocked = true;
                _boss.Patterns.Add(BossStateType.AttackLaser);
                _boss.Patterns.Add(BossStateType.AttackBomb);
            }

            if (!_boss.Phase3Executed && hpRate <= 5f)
            {
                _boss.Phase3Executed = true;
                _boss.CurrentType = BossStateType.AttackAOE;
                _stateMachine.ChangeState(BossStateType.AttackAOE); 
                return;
            }
        }
    }
}