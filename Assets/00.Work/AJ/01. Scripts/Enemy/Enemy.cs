using System;
using UnityEngine;
using Unity.Behavior;
using UnityEngine.AI;
using _00.Work.SYH._02Script.ETG;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Agent, IPoolable
{
    //public BehaviorGraphAgent BtAgent { get; private set; }
    public string ItemName => enemySO.enemyName;
    public GameObject GameObject => gameObject;
    public Transform target { get; private set; }
    [field:SerializeField]public Transform FirePos { get; private set; }
    [field:SerializeField] public EnemySO enemySO { get; private set; }
    [SerializeField] public LayerMask playerMask;
    [SerializeField] public LayerMask whatIsWall;
    public float ChaseRange { get; private set; }    
    public float AttackRange { get; private set; }
    public Vector2 AttackBoxRange { get; private set; }
    public float DeathRange { get; private set; }
    private bool canFlip = true;
    public bool isDead { get; set; } = false;
    public bool isHit { get; set; } = false;
    public ParticleSystem vfx = null;

    #region Components
    public Animator AnimCompo { get; private set; }
    public EnemyRenderer VisualCompo { get; private set; }
    public NavMeshAgent AgentCompo { get; private set; }
    public Rigidbody2D RbCompo { get; private set; }
    public Collider2D ColliderCompo { get; private set; }
    public HealthSystem HealthCompo { get; private set; }
    [field:SerializeField]public WayPoints wayPoints { get; private set; }
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
        target = FindAnyObjectByType<Player>().transform;
        wayPoints = FindAnyObjectByType<WayPoints>().GetComponent<WayPoints>();
        //BtAgent = GetComponent<BehaviorGraphAgent>();

        AgentCompo.updateRotation = false;
        AgentCompo.updateUpAxis = false;
        
        
        ChaseRange = enemySO.chaseRange;
        AttackRange = enemySO.attackRange;
        if(enemySO.useBoxRange)
            AttackBoxRange = enemySO.boxRange;

        if (FirePos == null && enemySO.enemyType == EnemyType.Ranged)
        {
            Debug.LogError("Error");
        }
        if (enemySO.assassinData != null && enemySO.assassinData.vanishVfx != null)
        {
            vfx = Instantiate(
                enemySO.assassinData.vanishVfx,
                transform.position,
                Quaternion.identity,
                transform
            );
        }
    }

    private void Start()
    {
        /*if (wayPoints != null) // 삭제
        {
            Vector3 spawnPos = wayPoints.GetRandomWayPoint();
            transform.position = spawnPos;
        }*/
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
        else
            return Physics2D.OverlapCircle(transform.position, AttackRange, playerMask);
    }
    public bool CheckDeathRange()
    {
        return Physics2D.OverlapCircle(transform.position, DeathRange);
    }
    // 플레이어가 시야 안에 있는지
    public bool IsPlayerInSight()
    {
        if (target == null) return false;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        float dist = Vector2.Distance(transform.position, target.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dist, whatIsWall);

        return hit.collider == null;
    }

    public bool IsOutScreen()
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        bool isOutScreen = screenPoint.x <= 0 || screenPoint.x >= Screen.width || screenPoint.y <= 0 || screenPoint.y >= Screen.height;
        return isOutScreen;
    }

    private void LateUpdate()
    {
        if(VisualCompo != null && canFlip && AgentCompo != null)
        {
            if (AgentCompo.velocity.sqrMagnitude > 0.01f)
            {
                VisualCompo.Flip(AgentCompo.velocity); 
            }
            else if (CheckChaseRange() && target != null)
            {
                VisualCompo.Flip(target.position - transform.position);
            }
        }
    }

    public void ChangeFlip(bool value)
    {
        canFlip = value;
    }

    public void ResetItem()
    {
        isDead = false;
        canFlip = true;
        /*if (wayPoints != null)
        {
            Vector3 spawnPos = wayPoints.GetRandomWayPoint();
            transform.position = spawnPos;
        }*/
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if(GetComponent<HealthSystem>() != null && enemySO != null)
            transform.GetComponent<HealthSystem>().SetMaxHealth(enemySO.health);
    }

    private void OnDrawGizmos()
    {
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
