using System.Collections.Generic;
using _00.Work.AJ._01._Scripts.Boss2.FSM;
using _00.Work.AJ._01._Scripts.BossClones;

namespace _00.Work.AJ._01._Scripts.BossClone
{
    public class BossCloneStateMachine
    {
        private Dictionary<BossCloneStateType, BossCloneState> _stateDictionary = new Dictionary<BossCloneStateType, BossCloneState>();

        public BossCloneState CurrentState { get; private set; }

        public void AddState(BossCloneStateType type, BossCloneState boss)
        {
            _stateDictionary.Add(type, boss);
        }
        public void Initialized(BossCloneStateType type)
        {
            CurrentState = _stateDictionary[type];
            CurrentState?.Enter();
        }
        public void ChangeState(BossCloneStateType stateType)
        {
            CurrentState?.Exit();
            CurrentState = _stateDictionary[stateType];
            CurrentState?.Enter();
        }
    }
}

public enum BossCloneStateType
{
    Idle,
    Chase,
    Attack,
    Death,
    Hit,
    
}