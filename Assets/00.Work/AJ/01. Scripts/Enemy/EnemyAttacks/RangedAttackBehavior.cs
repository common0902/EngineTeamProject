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
            GameObject obj = Object.Instantiate(
                _enemy.enemySO.rangedData.bulletData.projectilePrefab,
                _enemy.FirePos.position,
                Quaternion.identity
            );
            Bullet bullet = obj.GetComponent<Bullet>();

            bullet.SetUp(direction, _enemy, _enemy.enemySO.rangedData.bulletData.bulletSpeed);

            yield return new WaitForSeconds(0.1f);
            _canAttack = true;
        }
    }

    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }
}
