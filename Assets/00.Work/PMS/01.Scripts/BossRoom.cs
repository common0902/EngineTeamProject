using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

public class BossRoom : Room
{
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject boss;
    public HealthSystem HealthSystem { get; private set; }
    
    protected override void Awake()
    {
        base.Awake(); 
        HealthSystem = boss.GetComponent<HealthSystem>();
    }
    
    protected override void Start()
    {
        hasEnemies = true;
        enemyCount = 1;
    }

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        if (!isCleared)
            OnInRoom?.Invoke();
        SoundManager.Instance.PlaySound("FinalBossBGM");
        bossHealthBar.SetActive(true);
        boss.SetActive(true);
    }

    private void HandleBossDied()
    {
        //OnEnemyDied();
        portal.SetActive(true);
        box.SetActive(true);
        SoundManager.Instance.PlaySound("MainGameBGM");
    }

    public override void OnEnemyDied()
    {
        base.OnEnemyDied();
        HandleBossDied();
    }
}
