using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class AssassinAttackBehavior : IEnemyAttackBehavior
{
    private Enemy _enemy;
    private HealthSystem _targetHealth;
    private SpriteRenderer _renderer;
    private WaitForSeconds _attackDelay;
    private bool _canAttack = true;

    public bool IsAttackAnimationEnd { get; set; }

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _targetHealth = _enemy.target.GetComponent<HealthSystem>();
        _renderer = _enemy.GetComponentInChildren<SpriteRenderer>();
        _attackDelay = new WaitForSeconds(_enemy.enemySO.attackDelay);
    }
    
    public IEnumerator ExecuteAttack(Vector2 direction)
    {
        if (!_canAttack || _enemy.target == null)
            yield break;
        _enemy.ColliderCompo.enabled = false;
        _canAttack = false;

        _targetHealth.Damage(_enemy.enemySO.damage);
        
        IsAttackAnimationEnd = false;
        while (!IsAttackAnimationEnd)
            yield return null;

        IsAttackAnimationEnd = false;
        _canAttack = true;
    }

    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }

    private void ResetAssassinState()
    {
        if (_renderer != null)
            _renderer.color = new Color(1, 1, 1, 1f);

        _enemy.HealthCompo.enabled = true;
        _enemy.ColliderCompo.enabled = true;
        _canAttack = true;
        IsAttackAnimationEnd = true;
    }
    public void Vanish()
    {
        _enemy.AssashinVfx.Play();
        
        if (_renderer != null)
            _renderer.color = new Color(1f, 1f, 1f, 0f);

        _enemy.HealthCompo.enabled = false;
        _enemy.ColliderCompo.enabled = false;
    }

    public void AppearBehind()
    {
        if (_enemy.target == null)
            return;

        Vector3 playerPos = _enemy.target.position;
        Vector3 dir = (playerPos - _enemy.transform.position).normalized;

        float dist = _enemy.enemySO.assassinData.appearBehindDistance;
        Vector3 appearPos = playerPos - dir * dist;

        if (_enemy.AgentCompo != null && _enemy.AgentCompo.enabled)
            _enemy.AgentCompo.Warp(appearPos);
        else
            _enemy.transform.position = appearPos;

        _enemy.VisualCompo.Flip(playerPos - _enemy.transform.position);

        if (_renderer != null)
            _renderer.color = new Color(1f, 1f, 1f, 1f);

        _enemy.HealthCompo.enabled = true;
        _enemy.ColliderCompo.enabled = true;
    }
}
