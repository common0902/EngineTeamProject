using UnityEngine;

public class BossRoom : Room
{
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject boss;
    protected override void Start()
    {
        hasEnemies = true;
        enemyCount = 1;
    }

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        bossHealthBar.SetActive(true);
        boss.SetActive(true);
    }

    private void HandleBossDied()
    {
        OnEnemyDied();
        portal.SetActive(true);
        box.SetActive(true);
    }

    public override void OnEnemyDied()
    {
        base.OnEnemyDied();
    }
}
