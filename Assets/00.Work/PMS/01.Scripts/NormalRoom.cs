using System;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

public class NormalRoom : Room
{
    
    protected override void Start()
    {
        base.Start();
    }
    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        if (!isCleared)
            OnInRoom?.Invoke();
        SoundManager.Instance.PlaySound("BattleBGM");
    }

    public override void OnEnemyDied()
    {
        base.OnEnemyDied();
        SoundManager.Instance.PlaySound("MainGameBGM");
    }
}
