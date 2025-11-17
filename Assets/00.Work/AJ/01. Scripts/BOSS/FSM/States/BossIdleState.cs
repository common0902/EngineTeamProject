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
            if (!_boss.HasStarted)
            {
                _waitTime = 0f;
                _boss.HasStarted = true;
            }
            else
            {
                _waitTime = Random.Range(3f, 5f); 
            }
        }

        public override void Update()
        {
            base.Update();
            
            Debug.Log($"Current Pattern : {_boss.CurrentType}");
            
            _timer += Time.deltaTime;
            if (_timer > _waitTime)
            {
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
            HandlePhase();
            
            if (_boss.Phase3Executed)
                return;
            
            var pool = _boss.Patterns;
            BossStateType next = pool[Random.Range(0, pool.Count)];
            Debug.Log($"Next Pattern : {next}");
            _boss.CurrentType = next;
            if (next == BossStateType.AttackLaser || next == BossStateType.AttackBomb)
            {
                _boss.NextStateAfterVanish = next;
                _stateMachine.ChangeState(BossStateType.Vanish);
                return;
            }
        
            if (next == BossStateType.AttackMelee || next == BossStateType.AttackDash)
            {
                _stateMachine.ChangeState(BossStateType.Chase);
                return;
            }

            if (next == BossStateType.AttackRange || next == BossStateType.AttackSummon)
            {
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

            if (!_boss.Phase3Executed && hpRate <= 10f)
            {
                _boss.Phase3Executed = true;
                _stateMachine.ChangeState(BossStateType.AttackAOE); 
                return;
            }
        }
    }
}