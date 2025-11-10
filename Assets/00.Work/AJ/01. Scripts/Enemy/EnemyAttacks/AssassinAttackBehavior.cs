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
    private Coroutine _assassinRoutine;

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
        if (_assassinRoutine != null || !_canAttack)
            yield break;

        _canAttack = false;

        _enemy.HealthCompo.enabled = false;
        _enemy.ColliderCompo.isTrigger = true;
        _enemy.vfx.Play();
        if (_renderer != null)
            _renderer.color = new Color(1, 1, 1, 0);

        float hideTime = _enemy.enemySO.assassinData.hideDuration;
        if (_enemy.enemySO.assassinData.randomHideDuration)
        {
            hideTime = Random.Range(_enemy.enemySO.assassinData.minHideDuration, _enemy.enemySO.assassinData.hideDuration);
        }
        yield return new WaitForSeconds(hideTime);

        Transform target = _enemy.target;
        if (target == null)
            yield break;

        Vector3 playerPos = target.position;
        Vector3 toPlayer = (playerPos - _enemy.transform.position).normalized;
        float behindDist = _enemy.enemySO.assassinData.appearBehindDistance;
        Vector3 appearPos = playerPos - toPlayer * behindDist;

        if (_enemy.AgentCompo != null && _enemy.AgentCompo.enabled)
            _enemy.AgentCompo.Warp(appearPos);
        else
            _enemy.transform.position = appearPos;

        _enemy.VisualCompo.Flip(playerPos - _enemy.transform.position);

        if (_renderer != null)
            _renderer.color = new Color(1, 1, 1, 1);

        if (_enemy.CheckAttackRange())
        {
            _targetHealth = target.GetComponent<HealthSystem>();
            if (_targetHealth != null)
                _targetHealth.Damage(_enemy.enemySO.damage);
        }

        _enemy.HealthCompo.enabled = true;
        _enemy.ColliderCompo.isTrigger = false;

        yield return _attackDelay;

        _canAttack = true;
        _assassinRoutine = null;
    }

    public void OnAttackAnimationEnd()
    {
        IsAttackAnimationEnd = true;
    }
}
