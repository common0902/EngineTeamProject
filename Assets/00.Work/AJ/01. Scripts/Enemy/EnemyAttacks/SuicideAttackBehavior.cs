using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEditor;
using UnityEngine;

public class SuicideAttackBehavior : IEnemyAttackBehavior
{
    private Enemy _enemy;
    private bool _canAttack = true;
    private bool _isExploding = false;
    private SpriteRenderer _spriteRenderer;
    public bool IsAttackAnimationEnd { get; set; }

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _spriteRenderer = enemy.GetComponentInChildren<SpriteRenderer>();
    }

    public IEnumerator ExecuteAttack(Vector2 direction)
    {
        if (!_canAttack)
            yield break;

        _canAttack = false;
        _isExploding = true;
        
        if (_enemy.AgentCompo != null)
            _enemy.AgentCompo.isStopped = true;
        
        if (_enemy.RbCompo != null)
            _enemy.RbCompo.linearVelocity = Vector2.zero;

        float delay = _enemy.enemySO.suicideAttackerData?.explosionDelay ?? 0.2f;
        
        yield return CountdownEffect(delay);

        if (_enemy.enemySO.suicideAttackerData != null)
        {
            ParticleSystem particle = Object.Instantiate(
                _enemy.enemySO.suicideAttackerData.particleSystem,
                _enemy.transform.position,
                Quaternion.identity
            );
            particle.Play();
            Object.Destroy(particle.gameObject, 2f);
            
            Explode();
        }

        _enemy.HealthCompo.Damage(float.MaxValue);
        _isExploding = false;
        IsAttackAnimationEnd = true;
    }

    private IEnumerator CountdownEffect(float duration)
    {
        float elapsed = 0f;
        float blinkSpeed = 0.1f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            
            blinkSpeed = Mathf.Lerp(0.3f, 0.05f, progress);
            
            if (_spriteRenderer != null)
            {
                float t = Mathf.PingPong(Time.time / blinkSpeed, 1f);
                _spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
            }
            
            yield return null;
        }
        
        _enemy.transform.localScale = Vector3.one;
    }

    private void Explode()
    {
        float radius = _enemy.enemySO.suicideAttackerData.explosionRadius;
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            _enemy.transform.position, 
            radius
        );

        foreach (var hit in hits)
        {
            HealthSystem health = hit.GetComponent<HealthSystem>();
            if (health != null && hit.gameObject != _enemy.gameObject)
            {
                health.Damage(_enemy.enemySO.damage);
            }
        }
    }

    public void OnAttackAnimationEnd()
    {
        if (!_isExploding)
            IsAttackAnimationEnd = true;
    }
}
