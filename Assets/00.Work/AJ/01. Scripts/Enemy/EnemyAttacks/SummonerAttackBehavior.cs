using System.Collections;
using _00.Work.SYH._02Script.ETG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class SummonerAttackBehavior : IEnemyAttackBehavior
{
    private Enemy _enemy;
    private int _currentCount = 0;
    private WaitForSeconds _attackDelay;

    public bool IsAttackAnimationEnd { get; set; }

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _attackDelay = new WaitForSeconds(_enemy.enemySO.attackDelay);
    }

    public IEnumerator ExecuteAttack(Vector2 direction)
    {
        if (_enemy.enemySO.summonerData.maxSummonCount > _currentCount)
        {
            yield return _attackDelay;

            Vector2 summonPos = Vector2.zero;
            bool found = false;

            for (int i = 0; i < 30; i++)
            {
                Vector2 randomDir = Random.insideUnitCircle;
                float distance = Random.Range(0f, _enemy.enemySO.summonerData.summonRange);
                summonPos = (Vector2)_enemy.transform.position + randomDir * distance;

                RaycastHit2D pathCheck = Physics2D.Raycast(_enemy.transform.position, randomDir, distance, _enemy.whatIsWall);
                bool posCheck = Physics2D.OverlapCircle(summonPos, 0.5f, _enemy.whatIsWall) == null;
                if (pathCheck.collider == null && posCheck)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                int i = Random.Range(0, _enemy.enemySO.summonerData.summonPrefab.Length);
                GameObject summon = Object.Instantiate(_enemy.enemySO.summonerData.summonPrefab[i], summonPos, Quaternion.identity);
                _currentCount++;

                Enemy summonEnemy = summon.GetComponent<Enemy>();
                if (_enemy.enemySO.summonerData.isFade)
                {
                    SpriteRenderer enemyRenderer = summon.GetComponentInChildren<SpriteRenderer>();
                    enemyRenderer.color = new Color(1, 1, 1, 0);
                    enemyRenderer.DOFade(1, 1);
                }

                HealthSystem summonHealth = summonEnemy.HealthCompo;
                if (summonHealth != null)
                {
                    summonHealth.SetMaxHealth(summonEnemy.enemySO.health);
                    summonHealth.OnDead += () => _currentCount--;
                }

                if (summonEnemy != null)
                {
                    HealthBarManager.Instance.RegisterEnemy(summonEnemy);
                }

                if (_enemy.enemySO.summonerData.isLifeTimeInChildren)
                    Object.Destroy(summon, _enemy.enemySO.summonerData.summonLifeTime);
            }
        }
        else
        {
            _enemy.ChangeAttackRange(0.0f);
            _enemy.ChangeChaseRange(0.0f);
            yield return new WaitForSeconds(_enemy.enemySO.summonerData.waitforNextSummon);
            _currentCount = 0;
            _enemy.ChangeChaseRange(_enemy.enemySO.chaseRange);
            _enemy.ChangeAttackRange(_enemy.enemySO.attackRange);
        }
    }
    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }
}
