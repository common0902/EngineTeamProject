using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrapperAttackBehavior : IEnemyAttackBehavior
{
    private static Dictionary<Enemy, List<Trap>> _allTraps = new();
    private Enemy _enemy;
    private float _lastTrapTime;
    private WaitForSeconds _attackDelay;
    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _attackDelay = new WaitForSeconds(_enemy.enemySO.attackDelay);
        _lastTrapTime = -_enemy.enemySO.trapperData.trapPlaceInterval;
        
        if (!_allTraps.ContainsKey(_enemy))
        {
            _allTraps[_enemy] = new List<Trap>();
        }
    }

    public IEnumerator ExecuteAttack(Vector2 direction)
    {
        CleanupDestroyedTraps();
        
        if (Time.time - _lastTrapTime < _enemy.enemySO.trapperData.trapPlaceInterval)
            yield break;

        if (_allTraps[_enemy].Count >= _enemy.enemySO.trapperData.maxTraps)
        {
            if (_allTraps[_enemy].Count > 0 && _allTraps[_enemy][0] != null)
            {
                Object.Destroy(_allTraps[_enemy][0].gameObject);
                _allTraps[_enemy].RemoveAt(0);
            }
        }


        GameObject trapObj = Object.Instantiate(_enemy.enemySO.trapperData.trapPrefab, _enemy.transform.position, Quaternion.identity);
        
        Trap trap = trapObj.GetComponent<Trap>();
        if (trap != null)
        {
            trap.Initialize(_enemy.enemySO.trapperData);
            _allTraps[_enemy].Add(trap);
        }

        if (_enemy.enemySO.trapperData.placeTrapVFX != null)
        {
            ParticleSystem vfx = Object.Instantiate(
                _enemy.enemySO.trapperData.placeTrapVFX,
                _enemy.transform.position,
                Quaternion.identity
            );
            vfx.Play();
            Object.Destroy(vfx.gameObject, 2f);
        }

        _lastTrapTime = Time.time;

        yield return _attackDelay;
    }

    private void CleanupDestroyedTraps()
    {
        if (!_allTraps.ContainsKey(_enemy)) return;
        _allTraps[_enemy].RemoveAll(trap => trap == null || trap.gameObject == null);
    }

    private void CleanUpAllTraps()
    {
        if (!_allTraps.ContainsKey(_enemy)) return;
        
        foreach (var trap in _allTraps[_enemy])
        {
            if (trap != null)
                Object.Destroy(trap.gameObject);
        }
        
        _allTraps.Remove(_enemy);
    }
    public int GetTrapCount()
    {
        CleanupDestroyedTraps();
        return _allTraps.ContainsKey(_enemy) ? _allTraps[_enemy].Count : 0;
    }
    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }

    public bool IsAttackAnimationEnd { get; set; }
}
