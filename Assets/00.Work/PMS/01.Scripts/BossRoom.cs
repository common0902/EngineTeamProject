using UnityEngine;

public class BossRoom : Room
{
    protected override void Start()
    {
        hasEnemies = true;
        enemyCount = 1;
    }

    public override void OnPlayerEnter()
    {
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
