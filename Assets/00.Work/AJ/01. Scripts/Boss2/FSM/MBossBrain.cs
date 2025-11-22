using _00.Work.AJ._01._Scripts.Boss2.FSM.States;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM
{
    [RequireComponent(typeof(MiddleBoss))]
    public class MiddleBossBrain : MonoBehaviour
    {
        private MiddleBossStateMachine _stateMachine;
        private MiddleBoss _boss;
        
         private void Awake()
        {
            _boss = GetComponent<MiddleBoss>();
            _stateMachine = new MiddleBossStateMachine();
            
            _stateMachine.AddState(MiddleBossStateType.Idle, new MiddleBossIdleState(_boss, "Idle", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.Chase, new MiddleBossChaseState(_boss, "Chase", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.Attack, new MiddleBossAttackState(_boss, "Attack", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.Range, new MBossRangeState(_boss, "Range", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.CircleRange, new MBossCircleRangeState(_boss, "CircleRange", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.Vanish, new MBossVanishState(_boss, "Vanish", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.Appear, new MBossAppearState(_boss, "Appear", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.DashBefore, new MBossDashBeforeState(_boss, "DashBefore", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.Dash, new MBossDashState(_boss, "Dash", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.Hit, new MiddleBossHitState(_boss, "Hit", _stateMachine));
            _stateMachine.AddState(MiddleBossStateType.Death, new MiddleBossDeathState(_boss, "Death", _stateMachine));
        }
        private void Start()
        {
            _stateMachine.Initialized(MiddleBossStateType.Idle);
        }
        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}