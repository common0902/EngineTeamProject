using System;
using UnityEngine;

public class NormalRoom : Room
{
    public Action OnBattleWin;
    
    protected override void Start()
    {
        base.Start();
    }
    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        OnInRoom?.Invoke();
    }

    public override void OnEnemyDied()
    {
        base.OnEnemyDied();
        OnBattleWin?.Invoke();
    }
}
