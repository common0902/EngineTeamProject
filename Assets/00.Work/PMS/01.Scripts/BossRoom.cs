using UnityEngine;

public class BossRoom : Room
{

    protected override void Start()
    {
        base.Start();
        hasEnemies = true;
        enemyCount = 1;   // 보스 1마리
    }

    public override void OnPlayerEnter()
    {
        // 기본 Room 입장 로직(문 잠금 등) 먼저 실행
        base.OnPlayerEnter();

       
    }

    private void HandleBossDied()
    {
        OnEnemyDied();
    }

    public override void OnEnemyDied()
    {
        base.OnEnemyDied();
    }
}
