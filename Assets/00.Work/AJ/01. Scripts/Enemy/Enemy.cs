using System;
using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Agent, IPoolable
{
    //public BehaviorGraphAgent BtAgent { get; private set; }
    public string ItemName => enemySO.enemyName;
    public GameObject GameObject => gameObject;
    public Transform Target { get; private set; }
    [field:SerializeField]public Transform FirePos { get; private set; }
    [field:SerializeField] public EnemySO enemySO { get; private set; }
    [SerializeField] public LayerMask playerMask;
    [SerializeField] public LayerMask whatIsWall;
    public float ChaseRange { get; private set; }    
    public float AttackRange { get; private set; }
    public Vector2 AttackBoxRange { get; private set; }
    public float DeathRange { get; private set; }
    private bool _canFlip = true;
    public bool IsDead { get; set; }
    public bool IsHit { get; set; } = false;
    public ParticleSystem AssashinVfx { get; set; }
    public float _speed;

    private bool _isCheckingDespawn = false;

    #region Components
    public Animator AnimCompo { get; private set; }
    public EnemyRenderer VisualCompo { get; private set; }
    public NavMeshAgent AgentCompo { get; private set; }
    public Rigidbody2D RbCompo { get; private set; }
    public Collider2D ColliderCompo { get; private set; }
    public HealthSystem HealthCompo { get; private set; }
    [field:SerializeField]public WayPoints WayPoints { get; private set; }
    #endregion
    
    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        AnimCompo = GetComponentInChildren<Animator>();
        AgentCompo = GetComponent<NavMeshAgent>();
        VisualCompo = GetComponentInChildren<EnemyRenderer>();
        RbCompo = GetComponent<Rigidbody2D>();
        ColliderCompo = GetComponent<Collider2D>();
        HealthCompo = GetComponent<HealthSystem>();
        Target = FindAnyObjectByType<Player>().transform;
        WayPoints = FindAnyObjectByType<WayPoints>().GetComponent<WayPoints>();
        //BtAgent = GetComponent<BehaviorGraphAgent>();
        _speed = AgentCompo.speed;

        AgentCompo.enabled = true;
        AgentCompo.updateRotation = false;
        AgentCompo.updateUpAxis = false;
        
        ChaseRange = enemySO.chaseRange;
        AttackRange = enemySO.attackRange;
        if(enemySO.useBoxRange)
            AttackBoxRange = enemySO.boxRange;

        if (FirePos == null && enemySO.enemyType == EnemyType.Ranged)
        {
            Debug.LogError("FirePos is null");
        }
        
        if (enemySO.assassinData != null && enemySO.assassinData.vanishVfx != null)
        {
            AssashinVfx = Instantiate(
                enemySO.assassinData.vanishVfx,
                transform.position,
                Quaternion.identity,
                transform
            );
        }
    }
    public void ChangeChaseRange(float value) => ChaseRange = value;
    public void ChangeAttackRange(float value) => AttackRange = value;
    public bool CheckChaseRange()
    {
        return Physics2D.OverlapCircle(transform.position, ChaseRange, playerMask) ;
    }
    public bool CheckAttackRange()
    {
        if (enemySO.useBoxRange)
            return Physics2D.OverlapBox(transform.position, AttackBoxRange, 0f, playerMask);
        return Physics2D.OverlapCircle(transform.position, AttackRange, playerMask);
    }
    public bool CheckDeathRange()
    {
        return Physics2D.OverlapCircle(transform.position, DeathRange);
    }
    // 플레이어가 시야 안에 있는지
    public bool IsPlayerInSight()
    {
        if (Target == null) return false;

        Vector2 dir = (Target.transform.position - transform.position).normalized;
        float dist = Vector2.Distance(transform.position, Target.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dist, whatIsWall);

        return hit.collider == null;
    }

    public bool IsOutScreen()
    {
        if (Camera.main != null)
        {
            Debug.Log("dfjakf");
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            bool isOutScreen = screenPoint.x <= 0 || screenPoint.x >= Screen.width || screenPoint.y <= 0 || screenPoint.y >= Screen.height;
            return isOutScreen;
        }

        return false;
    }

    private void LateUpdate()
    {
        if(VisualCompo != null && _canFlip && AgentCompo != null)
        {
            if (AgentCompo.velocity.sqrMagnitude > 0.01f)
            {
                VisualCompo.Flip(AgentCompo.velocity); 
            }
            else if (CheckChaseRange() && Target != null)
            {
                VisualCompo.Flip(Target.position - transform.position);
            }
        }
    }

    public void ChangeFlip(bool value)
    {
        _canFlip = value;
    }

    public void ResetItem()
    {
        IsDead = false;
        _canFlip = true;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if(GetComponent<HealthSystem>() != null && enemySO != null)
            transform.GetComponent<HealthSystem>().SetMaxHealth(enemySO.health);
    }

    private void OnDrawGizmos()
    {
        if (enemySO == null) return;
        if (enemySO.useBoxRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, enemySO.boxRange.y);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, transform.GetComponent<NavMeshAgent>().stoppingDistance);
        }
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemySO.chaseRange);

        Gizmos.color = Color.red;
        if (!enemySO.useBoxRange)
            Gizmos.DrawWireSphere(transform.position, enemySO.attackRange);
        else
            Gizmos.DrawWireCube(transform.position, enemySO.boxRange);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, enemySO.deathRange);

        if (enemySO.enemyType == EnemyType.Summoner)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, enemySO.summonerData.summonRange);
        }
    }
#endif
}
