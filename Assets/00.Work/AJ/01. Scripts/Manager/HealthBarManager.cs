using _00.Work.SYH._02Script.ETG;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoSingleton<HealthBarManager>
{
    public GameObject healthbarPrefab;
    private List<GameObject> _healthbars = new();
    public Dictionary<Enemy, Healthbar> dict = new();
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
        if (enemy == null) return;
        if (dict.ContainsKey(enemy)) return;
        
        GameObject bar = Instantiate(healthbarPrefab, transform);
        Healthbar hpbar = bar.GetComponent<Healthbar>();
        hpbar.Init(enemy.GetComponent<HealthSystem>());
        _healthbars.Add(bar);
        dict.Add(enemy, hpbar);
    }

    public void UnRegisterEnemy(Enemy enemy)
    {
        if (enemy == null) return;
        
        if (dict.ContainsKey(enemy))
        {
            Healthbar hpBar = dict[enemy];
            if (hpBar != null && hpBar.gameObject != null)
            {
                GameObject barObject = hpBar.gameObject;
                _healthbars.Remove(barObject);
                Destroy(barObject);
            }

            dict.Remove(enemy);
        }
    }
    private void LateUpdate()
    {
        foreach (var dic in dict)
        {
            if (dic.Key == null || dic.Value == null)
                continue;

            Vector3 worldPos = dic.Key.transform.position + new Vector3(0, -1.3f, 0);
            dic.Value.transform.position = worldPos;
        }
    }
}
