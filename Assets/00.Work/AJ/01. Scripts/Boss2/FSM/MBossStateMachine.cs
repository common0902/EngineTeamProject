using System.Collections.Generic;
using _00.Work.AJ._01._Scripts.BOSS.FSM;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM
{
    public class MiddleBossStateMachine
    {
        private Dictionary<MiddleBossStateType, MiddleBossState> _stateDictionary = new Dictionary<MiddleBossStateType, MiddleBossState>();

        public MiddleBossState CurrentState { get; private set; }

        public void AddState(MiddleBossStateType type, MiddleBossState boss)
        {
            _stateDictionary.Add(type, boss);
        }
        public void Initialized(MiddleBossStateType type)
        {
            CurrentState = _stateDictionary[type];
            CurrentState?.Enter();
        }
        public void ChangeState(MiddleBossStateType state)
        {
            CurrentState?.Exit();
            CurrentState = _stateDictionary[state];
            CurrentState?.Enter();
        }
    }
}