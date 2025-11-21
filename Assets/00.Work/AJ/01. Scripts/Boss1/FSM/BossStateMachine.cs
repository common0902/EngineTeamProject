using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM
{
    public class BossStateMachine
    {
        private Dictionary<BossStateType, BossState> _stateDictionary = new Dictionary<BossStateType, BossState>();

        public BossState CurrentState { get; private set; }

        public void AddState(BossStateType type, BossState boss)
        {
            _stateDictionary.Add(type, boss);
        }
        public void Initialized(BossStateType type)
        {
            CurrentState = _stateDictionary[type];
            CurrentState?.Enter();
        }
        public void ChangeState(BossStateType state)
        {
            CurrentState?.Exit();
            CurrentState = _stateDictionary[state];
            CurrentState?.Enter();
        }
    }
}