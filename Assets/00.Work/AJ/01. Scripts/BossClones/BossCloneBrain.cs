using _00.Work.AJ._01._Scripts.BossClone;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    [RequireComponent(typeof(BossClone))]
    public class BossCloneBrain : MonoBehaviour
    {
        private BossCloneStateMachine _stateMachine;
        private BossClone _boss;
        
         private void Awake()
        {
            _boss = GetComponent<BossClone>();
            _stateMachine = new BossCloneStateMachine();
            
            _stateMachine.AddState(BossCloneStateType.Idle, new BossCloneIdleState(_boss, "Idle", _stateMachine));
            _stateMachine.AddState(BossCloneStateType.Chase, new BossCloneChaseState(_boss, "Chase", _stateMachine));
            _stateMachine.AddState(BossCloneStateType.Attack, new BossCloneAttackState(_boss, "Attack", _stateMachine));
            _stateMachine.AddState(BossCloneStateType.Hit, new BossCloneHitState(_boss, "Hit", _stateMachine));
            _stateMachine.AddState(BossCloneStateType.Death, new BossCloneDeathState(_boss, "Death", _stateMachine));
        }
        private void Start()
        {
            _stateMachine.Initialized(BossCloneStateType.Idle);
        }
        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}