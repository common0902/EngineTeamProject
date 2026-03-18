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
        base.OnEnemyDied(); // base가 activeBGM 중지
        SoundManager.Instance.PlaySound("MainGameBGM");
    }
}
