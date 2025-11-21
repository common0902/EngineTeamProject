using _00.Work.AJ._01._Scripts.BOSS.FSM.States;
using _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates;
using _00.Work.AJ._01._Scripts.Boss1.FSM.States;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM
{
    [RequireComponent(typeof(Boss))]
    public class BossBrain : MonoBehaviour
    {
        private BossStateMachine _stateMachine;
        private Boss _boss;

        private void Awake()
        {
            _boss = GetComponent<Boss>();
            _stateMachine = new BossStateMachine();

            _stateMachine.AddState(BossStateType.Idle, new BossIdleState(_boss, "Idle", _stateMachine));
            _stateMachine.AddState(BossStateType.Chase, new BossChaseState(_boss, "Chase", _stateMachine));
            _stateMachine.AddState(BossStateType.Vanish, new BossVanishState(_boss, "Vanish", _stateMachine));
            _stateMachine.AddState(BossStateType.Appear, new BossAppearState(_boss, "Appear", _stateMachine));
            _stateMachine.AddState(BossStateType.AttackMelee, new BossAttackMeleeState(_boss, "AttackMelee", _stateMachine));
            _stateMachine.AddState(BossStateType.AttackRange, new BossAttackRangeState(_boss, "AttackRange", _stateMachine));
            _stateMachine.AddState(BossStateType.AttackLaser, new BossAttackLaserState(_boss, "AttackLaser", _stateMachine));
            _stateMachine.AddState(BossStateType.AttackDash, new BossAttackDashState(_boss, "AttackDash", _stateMachine));
            _stateMachine.AddState(BossStateType.AttackSummon, new BossAttackSummonState(_boss, "AttackSummon", _stateMachine));
            _stateMachine.AddState(BossStateType.AttackBomb, new BossAttackBombState(_boss, "AttackBomb", _stateMachine));
            _stateMachine.AddState(BossStateType.AttackAOE, new BossAttackAOEState(_boss, "AttackAOE", _stateMachine));
            _stateMachine.AddState(BossStateType.Hit, new BossHitState(_boss, "Hit", _stateMachine));
            _stateMachine.AddState(BossStateType.Dead, new BossDeathState(_boss, "Death", _stateMachine));
            _stateMachine.AddState(BossStateType.ReturnToIdle, new BossAttackToIdleState(_boss, "StopMove", _stateMachine));
        }
        private void Start()
        {
            _stateMachine.Initialized(BossStateType.Idle);
        }
        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}
public enum BossStateType
{
    Idle,
    Chase,
    Vanish,
    Appear,
    
    AttackMelee,
    AttackRange,
    AttackLaser,
    AttackDash,
    AttackSummon,
    AttackBomb,
    AttackAOE,
    
    ReturnToIdle,
    Hit,
    Dead,
}