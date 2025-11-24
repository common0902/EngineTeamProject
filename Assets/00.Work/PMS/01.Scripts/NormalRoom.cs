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
    }

    public override void OnEnemyDied()
    {
        base.OnEnemyDied();
        SoundManager.Instance.StopSound("BattleBGM");
        SoundManager.Instance.PlaySound("MainGameBGM");
    }
}
