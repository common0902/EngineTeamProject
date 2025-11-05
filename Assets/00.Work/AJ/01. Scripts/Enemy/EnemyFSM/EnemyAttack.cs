using DG.Tweening;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using _00.Work.SYH._02Script.ETG;

public class EnemyAttack : MonoBehaviour
{    
    private EnemyAnimator _enemyAnimator;
    public bool isAnimationEnd = false;
    private Enemy _enemy;
    private SpriteRenderer _renderer;
    private HealthSystem _targetHealth;
    private int currentCount = 0;
    private bool canAttack = true;
    private WaitForSeconds _attackDelay;
    private Coroutine _assassinRoutine;
    private float lastAttackTime = 0f;
    private void Awake()
    {
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        _enemy = GetComponent<Enemy>();
        _renderer = _enemy.GetComponentInChildren<SpriteRenderer>();

        _attackDelay = new WaitForSeconds(_enemy.enemySO.attackDelay);
    }
    private void Start()
    {
        _targetHealth = _enemy.target.GetComponent<HealthSystem>();

        _enemyAnimator.OnAttackTrigger += Attack;
        _enemyAnimator.OnAttackEndTrigger += OnAttackEnd;
    }

    private void OnAttackEnd() => isAnimationEnd = true;

    public void Attack()
    {
        lastAttackTime = Time.time;

        Vector2 dir = (_enemy.target.position - _enemy.transform.position).normalized;
        if (_enemy.enemySO.enemyType == EnemyType.Assassin && _assassinRoutine != null)
            return;
        if (!canAttack) return;
        switch (_enemy.enemySO.enemyType)
        {
            case EnemyType.Melee:
                StartCoroutine(MeleeAttack());
                break;
            case EnemyType.Ranged:
                StartCoroutine(RangedAttack(dir));
                break;
            case EnemyType.Dash:
                StartCoroutine(DashAttack(dir));
                break;
            case EnemyType.Summoner:
                StartCoroutine(SummonAttack(dir));
                break;
            case EnemyType.Assassin:
                _assassinRoutine = StartCoroutine(AssassinAttack(dir, _enemy.vfx));
                break;
            case EnemyType.SuisideAttacker:
                StartCoroutine(SuisideAttack(dir));
                break;
        }
    }
    public bool CanAttack()
    {
        return Time.time >= lastAttackTime + _enemy.enemySO.attackDelay;
    }
    private IEnumerator MeleeAttack()
    {
        if (_enemy.CheckAttackRange())
        {
            yield return _attackDelay;
            _targetHealth.Damage(_enemy.enemySO.damage);
        }
    }
    private IEnumerator RangedAttack(Vector2 dir)
    {
        yield return null;
        GameObject obj = Instantiate(_enemy.enemySO.rangedData.bulletData.projectilePrefab, _enemy._firePos.position, Quaternion.identity);
        Bullet bullet = obj.GetComponent<Bullet>();

        bullet.SetUp(dir, _enemy.enemySO);
    }
    private IEnumerator DashAttack(Vector2 dir)
    {
        _enemy.AgentCompo.isStopped = true;
        _enemy.ColliderCompo.isTrigger = true;
        
        _enemy.RbCompo.linearVelocity = dir * _enemy.enemySO.dashData.dashForce;
        yield return new WaitForSeconds(_enemy.enemySO.dashData.dashAttackTime);

        if(_enemy.CheckAttackRange())
            _targetHealth.Damage(_enemy.enemySO.damage);

        _enemy.RbCompo.linearVelocity = Vector2.zero;
        yield return _attackDelay;

        _enemy.ColliderCompo.isTrigger = false;
        _enemy.AgentCompo.isStopped = false;
    }
    private IEnumerator SummonAttack(Vector2 dir)
    {
        if (_enemy.enemySO.summonerData.maxSummonCount > currentCount)
        {
            yield return _attackDelay;

            Vector2 summonPos = Vector2.zero;
            bool found = false;
            
            for (int i = 0; i < 10; i++) 
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                float distance = Random.Range(0f, _enemy.enemySO.summonerData.summonRange);
                summonPos = (Vector2)_enemy.transform.position + randomDir * distance;

                RaycastHit2D pathCheck = Physics2D.Raycast(_enemy.transform.position, randomDir, distance, _enemy.whatIsWall);
                bool posCheck = Physics2D.OverlapCircle(summonPos, 0.5f, _enemy.whatIsWall) == null;
    
                if (pathCheck.collider == null && posCheck)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                int i = Random.Range(0, _enemy.enemySO.summonerData.summonPrefab.Length);
                GameObject summon = Instantiate(_enemy.enemySO.summonerData.summonPrefab[i], summonPos, Quaternion.identity);
                currentCount++;
                
                if (_enemy.enemySO.summonerData.isFade)
                {
                    SpriteRenderer enemyRenderer = summon.GetComponentInChildren<SpriteRenderer>();
                    enemyRenderer.color = new Color(1, 1, 1, 0);
                    enemyRenderer.DOColor(new Color(1, 1, 1, 1), 1);    
                }

                HealthSystem summonHealth = summon.GetComponent<HealthSystem>();
                if (summonHealth != null)
                {
                    summonHealth.OnDead += () => currentCount--; // 죽으면 다시 소환
                }
                
                Enemy summonEnemy = summon.GetComponent<Enemy>();
                if (summonEnemy != null)
                {
                    HealthBarManager.Instance.RegisterEnemy(summonEnemy);
                }
                
                if(_enemy.enemySO.summonerData.isLifeTimeInChildren)
                    Destroy(summon, _enemy.enemySO.summonerData.summonLifeTime);
            }
        }
        else
        {
            _enemy.ChangeAttackRange(0.0f);
            _enemy.ChangeChaseRange(0.0f);
            yield return new WaitForSeconds(_enemy.enemySO.summonerData.waitforNextSummon);
            currentCount = 0;
            _enemy.ChangeChaseRange(_enemy.enemySO.chaseRange);
            _enemy.ChangeAttackRange(_enemy.enemySO.attackRange);
        }
    }
    private IEnumerator AssassinAttack(Vector2 dir, ParticleSystem vfx)
    {
        canAttack = false;

        _enemy.HealthCompo.enabled = false;
        _enemy.ColliderCompo.isTrigger = true;
        vfx.Play();
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

        canAttack = true;
        _assassinRoutine = null;
    }
    private IEnumerator SuisideAttack(Vector2 dir)
    {
        canAttack = false;

        float delay = _enemy.enemySO.suicideAttackerData != null ? _enemy.enemySO.suicideAttackerData.explosionDelay : 0.2f;
        yield return new WaitForSeconds(delay);

        if (_enemy.enemySO.suicideAttackerData != null)
        {
            ParticleSystem particle = Instantiate(
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

        Destroy(transform.gameObject);        
    }

    private void OnDestroy()
    {
        _enemyAnimator.OnAttackTrigger -= Attack;
        _enemyAnimator.OnAttackEndTrigger -= OnAttackEnd;
    }

}
