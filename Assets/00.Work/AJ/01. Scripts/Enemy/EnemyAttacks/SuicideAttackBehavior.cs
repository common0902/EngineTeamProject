using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class SuicideAttackBehavior : IEnemyAttackBehavior
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
        if (!_canAttack)
            yield break;

        _canAttack = false;

        float delay = _enemy.enemySO.suicideAttackerData != null ? _enemy.enemySO.suicideAttackerData.explosionDelay : 0.2f;
        yield return new WaitForSeconds(delay);

        if (_enemy.enemySO.suicideAttackerData != null)
        {
            ParticleSystem particle = Object.Instantiate(
                _enemy.enemySO.suicideAttackerData.particleSystem,
                _enemy.transform.position,
                Quaternion.identity
            );

            float radius = _enemy.enemySO.suicideAttackerData != null ? _enemy.enemySO.suicideAttackerData.explosionRadius : 1.5f;
            Collider2D[] hits = Physics2D.OverlapCircleAll(_enemy.transform.position, radius);
            foreach (var hit in hits)
            {
                var playerHealth = hit.GetComponent<HealthSystem>();
                if (playerHealth != null)
                {
                    playerHealth.Damage(_enemy.enemySO.damage);
                }
            }

            particle.Play();
        }

        Object.Destroy(_enemy.transform.gameObject);
    }

    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }
}
