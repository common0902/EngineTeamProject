using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    [field:SerializeField] public List<Enemy> ActiveEnemies { get; private set; } = new();
    private WayPoints _spawnPoints;

    
    protected override void Awake()
    {
        base.Awake();
        ActiveEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
        foreach (var e in ActiveEnemies)
        {
            RegisterEnemy(e);
        }
    }

    private void Update()
    {
        ClearMissingObject();
    }

    [ContextMenu("Spawn")]
    public void SpawnSample()
    {
        SpawnEnemy("Ghoul");
    }

    public void RegisterEnemy(Enemy enemy) => InternalRegisterEnemy(enemy);
    public void UnRegisterEnemy(Enemy enemy) => InternalUnRegisterEnemy(enemy);
    public float TargetEnemyHealth(Enemy enemy)
    {
        return enemy.HealthCompo.Health;
    }

    public void InternalRegisterEnemy(Enemy enemy)
    {
        if (enemy == null || ActiveEnemies.Contains(enemy)) return;
        ActiveEnemies.Add(enemy);

        HealthBarManager.Instance.RegisterEnemy(enemy);
        
        enemy.HealthCompo.OnDead += () => OnEnemyDeath(enemy);
    }

    public void InternalUnRegisterEnemy(Enemy enemy)
    {
        if (enemy == null) return;
        if (ActiveEnemies.Remove(enemy))
        {
            HealthBarManager.Instance.UnRegisterEnemy(enemy);
        }
    }
    
    [ContextMenu("Get Alive Count")]
    public int GetAliveCount()
    {
        var active = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
        foreach (var e in active.ToList())
        {
            if(!e.IsPlayerInSight())
                active.Remove(e);
        }
        
        Debug.Log(active.Count);
        return active.Count;
    }

    [ContextMenu("Get Alive All Count")]
    public int GetAliveAllCount()
    {
        var active = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
        Debug.Log(active.Count);
        return active.Count;
    }
    private void OnEnemyDeath(Enemy enemy)
    {
        InternalUnRegisterEnemy(enemy);
        
        PoolManager.Instance.Push(enemy);
    }

    [ContextMenu("Get Count By Type")]
    public int GetCountByType(EnemyType enemyType) => ActiveEnemies.Count(e => e.enemySO.enemyType == enemyType);
    
    /// <summary>
    /// 에너미 스폰
    /// </summary>
    /// <param name="enemyName">이름으로 enemySO.name</param>
    /// <returns></returns>
    public Enemy SpawnEnemy(string enemyName)
    {
        IPoolable poolable = PoolManager.Instance.Pop(enemyName);
        Enemy enemy = poolable as Enemy;
        
        if (enemy != null)
        {
            _spawnPoints = FindAnyObjectByType<WayPoints>();
            Vector3 spawnPos = _spawnPoints.GetRandomWayPoint();
            enemy.transform.position = spawnPos;
            
            InternalRegisterEnemy(enemy);
            
            return enemy;
        }
        
        return null;
    }
    
    
    // 모든 적 멈추기
    public void FreezeAllEnemies(bool freeze = true)
    {
        foreach (var enemy in ActiveEnemies)
        {
            if (enemy.AgentCompo != null)
                enemy.AgentCompo.isStopped = freeze;
            
            if (enemy.RbCompo != null && freeze)
                enemy.RbCompo.linearVelocity = Vector2.zero;
            
            enemy.GetComponent<EnemyBrain>().enabled = freeze;
        }
    }
    
    // 가장 가까운 적 가지고 오기
    public Enemy GetNearestEnemy(Vector3 position)
    {
        return ActiveEnemies
            .OrderBy(e => Vector3.Distance(e.transform.position, position))
            .FirstOrDefault();
    }
    
    // 범위 안의 모든 적 가지고 오기
    public List<Enemy> GetEnemyInRange(Vector3 position, float radius)
    {
        return ActiveEnemies
            .Where(e => Vector3.Distance(e.transform.position, position) <= radius)
            .ToList();
    }

    [ContextMenu("Kill All")]
    // 모든 적 죽이기
    public void KillAllEnemies()
    {
        foreach (var enemy in ActiveEnemies.ToList())
        {
            if (enemy == null) continue;
            enemy.HealthCompo.Damage(float.MaxValue);
        }

        ActiveEnemies.Clear();
    }

    [ContextMenu("Clear All")]
    // 모두 없애기
    public void ClearAll()
    {
        foreach (var enemy in ActiveEnemies.ToList())
        {
            if (enemy == null) continue;
            Destroy(enemy.gameObject);
        }
        ActiveEnemies.Clear();
    }

    private void ClearMissingObject()
    {
        ActiveEnemies.RemoveAll(e => e == null);
    }
}
