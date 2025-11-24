using System.Collections;
using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEditor;
using UnityEngine;

public class SuicideAttackBehavior : IEnemyAttackBehavior
{
    private Enemy _enemy;
    private bool _canAttack = true;
    private bool _isExploding = false;
    private SpriteRenderer _spriteRenderer;
    public bool IsAttackAnimationEnd { get; set; }

    private SpriteRenderer _rangeSprite;
    private GameObject _rangeIndicator;
    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _spriteRenderer = enemy.GetComponentInChildren<SpriteRenderer>();

        CreateRangeIndicator();
    }

    private void CreateRangeIndicator()
    {
        if (_enemy.enemySO.suicideAttackerData.rangeIndicator != null)
        {
            _rangeIndicator = Object.Instantiate(_enemy.enemySO.suicideAttackerData.rangeIndicator,
                _enemy.transform.position, Quaternion.identity, _enemy.transform);
            _rangeSprite = _rangeIndicator.GetComponent<SpriteRenderer>();
            UpdateRangeIndicator();    
        }
    }

    private void UpdateRangeIndicator()
    {
        if (_rangeSprite == null || _enemy.enemySO.suicideAttackerData == null) 
            return;
        
        float diameter = _enemy.enemySO.suicideAttackerData.explosionRadius * 2f;
        _rangeIndicator.transform.localScale = new Vector3(diameter, diameter, 1f);
    }
    public IEnumerator ExecuteAttack(Vector2 direction)
    {
        if (!_canAttack)
            yield break;

        _canAttack = false;
        _isExploding = true;
        
        if (_enemy.AgentCompo != null && _enemy.AgentCompo.enabled)
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

        SoundManager.Instance.PlaySound("Bomb");
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
            
            float t = Mathf.PingPong(Time.time / blinkSpeed, 1f);
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
            }
            if (_rangeIndicator != null)
            {
                float baseAlpha = Mathf.Lerp(0.3f, 0.8f, progress);
                
                Color rangeColor = Color.Lerp(
                    new Color(1f, 0.5f, 0f, baseAlpha * 0.5f),  
                    new Color(1f, 0f, 0f, baseAlpha),
                    t
                );
                _rangeSprite.color = rangeColor;
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
            if (health != null)
            {
                health.Damage(_enemy.enemySO.damage);
            }
        }

        Object.Destroy(_rangeIndicator);
    }

    public void OnAttackAnimationEnd()
    {
        if (!_isExploding)
            IsAttackAnimationEnd = true;
    }
}
