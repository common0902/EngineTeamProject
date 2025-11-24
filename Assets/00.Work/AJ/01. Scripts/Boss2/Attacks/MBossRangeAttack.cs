using System.Collections;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.Attacks
{
    public class MBossRangeAttack : IMBossAttackBehaviour
    {
        private MiddleBoss _boss;
        private float _spreadAngle = 15f;
        public void Initialize(MiddleBoss boss)
        {
            _boss = boss;
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            Vector2 baseDir = direction.normalized;
            Vector2 leftDir = Rotate(baseDir,  _spreadAngle);
            Vector2 rightDir = Rotate(baseDir, -_spreadAngle);
            SoundManager.Instance.PlaySound("MiddleBossShoot");
            Transform fire = _boss.FirePos;
            CreateBullet(fire.position, baseDir);
            yield return new WaitForSeconds(0.1f);
            CreateBullet(fire.position, leftDir);
            yield return new WaitForSeconds(0.1f);
            CreateBullet(fire.position, rightDir);
            yield return new WaitForSeconds(0.1f);
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
            b.GetComponent<Bullet>().SetUp(_boss, dir, 5f);
        }
        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}