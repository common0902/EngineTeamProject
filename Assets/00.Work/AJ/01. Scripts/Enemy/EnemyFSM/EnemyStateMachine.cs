using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyStateType
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Hit,
    Dead,
}

public class EnemyStateMachine : MonoBehaviour
{
    private Dictionary<EnemyStateType, EnemyState> _stateDictionary = new Dictionary<EnemyStateType, EnemyState>();

    public EnemyState CurrentState { get; private set; }

    public void AddState(EnemyStateType type, EnemyState enemy)
    {
        _stateDictionary.Add(type, enemy);
    }
    public void Initialized(EnemyStateType type)
    {
        CurrentState = _stateDictionary[type];
        CurrentState?.Enter();
    }
    public void ChangeState(EnemyStateType state)
    {
        CurrentState?.Exit();
        CurrentState = _stateDictionary[state];
        CurrentState?.Enter();
    }
}
