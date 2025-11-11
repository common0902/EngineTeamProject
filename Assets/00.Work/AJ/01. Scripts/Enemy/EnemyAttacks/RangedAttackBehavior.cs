using System.Collections;
using UnityEngine;

public class RangedAttackBehavior : IEnemyAttackBehavior
{
    private Enemy _enemy;
    private bool _canAttack = true;

    public bool IsAttackAnimationEnd { get; set; }

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
    }

    public IEnumerator ExecuteAttack(Vector2 direction)
    {
        if (_enemy.CheckAttackRange() && _canAttack)
        {
            for (int i = 0; i < _enemy.enemySO.rangedData.bulletData.GetBulletCount(); i++)
            {
                GameObject obj = Object.Instantiate(
                    _enemy.enemySO.rangedData.bulletData.projectilePrefab,
                    _enemy.FirePos.position,
                    CalculateAngle()
                );
                Bullet bullet = obj.GetComponent<Bullet>();

                bullet.SetUp(direction, _enemy, _enemy.enemySO.rangedData.bulletData.bulletSpeed);
            }
            yield return new WaitForSeconds(0.1f);
            _canAttack = true;
        }
    }

    private void Shoot()
    {
        
    }

    private Quaternion CalculateAngle()
    {
        float spreadAngle = 0f;
        if (_enemy.enemySO.rangedData.bulletData.multibulletShoot)
            spreadAngle = Random.Range(-_enemy.enemySO.rangedData.bulletData.spreadAngle, _enemy.enemySO.rangedData.bulletData.spreadAngle);

        Quaternion bulletSpreadAngle = Quaternion.Euler(new Vector3(0, 0, spreadAngle));

        return bulletSpreadAngle * _enemy.FirePos.transform.rotation;
    }
    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }
}
