using _00.Work.SYH._02Script.ETG;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthBarManager : MonoSingleton<HealthBarManager>
{
    public List<Enemy> enemies = new List<Enemy>();
    public GameObject healthbarPrefab;
    public List<GameObject> healthbars = new List<GameObject>();
    private void Start()
    {
        SetupEnemyHealthBars();
    }
    public void SetupEnemyHealthBars()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                RegisterEnemy(enemy);
            }
        }
    }
    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy == null || enemies.Contains(enemy))
            return;

        enemies.Add(enemy);
        GameObject bar = Instantiate(healthbarPrefab, transform);
        Healthbar hpbar = bar.GetComponent<Healthbar>();
        hpbar.Init(enemy.GetComponent<HealthSystem>());
        healthbars.Add(bar);
    }
    private void LateUpdate()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null || healthbars[i] == null)
                continue;

            Vector3 worldPos = enemies[i].transform.position + new Vector3(0, -1.2f, 0); 
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            healthbars[i].transform.position = screenPos;
        }
    }
}
