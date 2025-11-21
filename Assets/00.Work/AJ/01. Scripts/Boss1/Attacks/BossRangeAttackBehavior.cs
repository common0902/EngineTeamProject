using System.Collections;
using UnityEditor.Searcher;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossRangeAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss;
        private int _bulletCount = 8; 
        private float _bulletSpeed = 5f;
        private float _spreadAngle = 360f;
        public void Initialize(Boss boss)
        {
            _boss = boss;
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            for (int i = 0; i < 2; i++)
            {
                FireBulletWave(direction, i);
                Debug.Log("Fire");
                yield return new WaitForSeconds(0.3f);
            }
            
            yield return new WaitForSeconds(0.5f);
        }

        private void FireBulletWave(Vector2 baseDirection, int waveIndex)
        {
            float angleStep = _spreadAngle / _bulletCount;
            float startAngle = waveIndex * 15f; 
            
            for (int i = 0; i < _bulletCount; i++)
            {
                float angle = startAngle + (angleStep * i);
                Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;
                SpawnBullet(bulletDirection);
            }
        }

        private void SpawnBullet(Vector2 direction)
        {
            GameObject bullet = Object.Instantiate(
                _boss.BulletPrefab, 
                _boss.FirePos.position, 
                Quaternion.identity
            );
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * _bulletSpeed;
            }
            var bulletDamage = bullet.GetComponent<BossBullet>();
            if (bulletDamage != null)
            {
                bulletDamage.Damage = _boss.Damage;
            }
            
            Object.Destroy(bullet, 5f);
        }

        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}