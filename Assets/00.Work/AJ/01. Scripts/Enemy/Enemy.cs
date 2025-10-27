using System;
using UnityEngine;
using Unity.Behavior;
using UnityEngine.AI;

public class Enemy : Agent, IPoolable
{
    //public BehaviorGraphAgent BtAgent { get; private set; }
    public string ItemName => enemySO.enemyName;
    public GameObject GameObject => gameObject;
    public Transform target { get; private set; }
    [field:SerializeField] public EnemySO enemySO { get; private set; }
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask whatIsWall;
    private bool canFlip = true;
    private bool isDead = false;
    #region Components
    public Animator AnimCompo { get; private set; }
    public EnemyRenderer VisualCompo { get; private set; }
    public NavMeshAgent AgentCompo { get; private set; }
    public Rigidbody2D RbCompo { get; private set; }
    [field:SerializeField]public WayPoints wayPoints { get; private set; }
    #endregion
    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        AnimCompo = GetComponentInChildren<Animator>();
        AgentCompo = GetComponent<NavMeshAgent>();
        VisualCompo = GetComponentInChildren<EnemyRenderer>();
        RbCompo = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        wayPoints = GameObject.FindGameObjectWithTag("WayPoints").GetComponent<WayPoints>();
        //BtAgent = GetComponent<BehaviorGraphAgent>();
        AgentCompo.updateRotation = false;
        AgentCompo.updateUpAxis = false;
    }

    private void Start()
    {
        if (wayPoints != null) // 삭제
        {
            Vector3 spawnPos = wayPoints.GetRandomWayPoint();
            transform.position = spawnPos;
        }
    }

    public bool CheckChaseRange()
    {
        return Physics2D.OverlapCircle(transform.position, enemySO.chaseRange, playerMask) && IsPlayerInSight();
    }
    public bool CheckAttackRange()
    {
        return Physics2D.OverlapCircle(transform.position, enemySO.attackRange, playerMask);
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
        if(VisualCompo != null && CheckChaseRange() && canFlip)
            VisualCompo.Flip(target.position - transform.position);
    }

    public void ChangeFlip(bool value)
    {
        canFlip = value;
    }

    public void ResetItem()
    {
        isDead = false;
        canFlip = true;
        if (wayPoints != null)
        {
            Vector3 spawnPos = wayPoints.GetRandomWayPoint();
            transform.position = spawnPos;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemySO.chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemySO.attackRange);
    }
#endif
}
