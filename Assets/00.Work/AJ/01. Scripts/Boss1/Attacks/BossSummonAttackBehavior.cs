using System.Collections;
using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossSummonAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss;
        private int _maxCount = 10;
        public void Initialize(Boss boss)
        {
            _boss = boss; 
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            if (_maxCount > _boss.CurrentSummonCount)
            {
                Vector2 summonPos = Vector2.zero;
                bool found = false;

                for (int i = 0; i < 30; i++)
                {
                    Vector2 randomDir = Random.insideUnitCircle;
                    float distance = Random.Range(0f, _boss.ChaseRange);
                    summonPos = (Vector2)_boss.transform.position + randomDir * distance;

                    RaycastHit2D pathCheck = Physics2D.Raycast(_boss.transform.position, randomDir, distance, _boss.whatIsWall);
                    bool posCheck = Physics2D.OverlapCircle(summonPos, 0.5f, _boss.whatIsWall) == null;
                    if (pathCheck.collider == null && posCheck)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    int i = Random.Range(0, _boss.summonPrefabs.Length);
                    SoundManager.Instance.PlaySound("BossSummon");
                    GameObject summon = Object.Instantiate(_boss.summonPrefabs[i], summonPos, Quaternion.identity);
                    _boss.CurrentSummonCount++;

                    global::Enemy summonEnemy = summon.GetComponent<global::Enemy>();
                    
                    HealthSystem summonHealth = summonEnemy.HealthCompo;
                    if (summonHealth != null)
                    {
                        summonHealth.SetMaxHealth(summonEnemy.enemySO.health / 3f);
                        summonHealth.OnDead += () => _boss.CurrentSummonCount--;
                    }

                    if (EnemyManager.Instance != null || HealthBarManager.Instance != null)
                        EnemyManager.Instance.RegisterEnemy(summonEnemy);
                }
            }
            else
            {
                yield return new WaitForSeconds(1f);
                _boss.CurrentSummonCount = 0;
            }
        }

        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}