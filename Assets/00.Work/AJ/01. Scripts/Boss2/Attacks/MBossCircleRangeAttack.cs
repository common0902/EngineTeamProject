using System.Collections;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.Attacks
{
    public class MBossCircleRangeAttack : IMBossAttackBehaviour
    {
        private MiddleBoss _boss;
        private int _bulletCount = 24;
        private float _delayBetweenShots = 0.1f;
        public void Initialize(MiddleBoss boss)
        {
            _boss = boss;
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            Vector2 curDir = direction.normalized;

            float stepAngle = -360f / _bulletCount; 

            Transform fire = _boss.FirePos;

            for (int i = 0; i < _bulletCount; i++)
            {
                CreateBullet(fire.position, curDir);

                curDir = Rotate(curDir, stepAngle);
                
                yield return new WaitForSeconds(_delayBetweenShots);
            }

            yield break; 
        }
        private Vector2 Rotate(Vector2 dir, float degrees)
        {
            float rad = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(rad);
            float cos = Mathf.Cos(rad);

            return new Vector2(dir.x * cos - dir.y * sin,dir.x * sin + dir.y * cos);
        }
        private void CreateBullet(Vector2 pos, Vector2 dir)
        {
            GameObject b = Object.Instantiate(_boss.BulletPrefabs, pos, Quaternion.identity);
            b.GetComponent<Bullet>().SetUp(_boss, dir, 3f);
        }
        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}