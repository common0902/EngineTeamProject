using UnityEngine;
using Unity.Behavior;
using UnityEngine.AI;

public class Enemy : Agent, IComponent
{
    public Animator AnimCompo { get; private set; } 

    public Transform target;
    public EnemyRenderer VisualCompo { get; private set; }
    [field: SerializeField] public NavMeshAgent AgentCompo { get; private set; }
    public BehaviorGraphAgent BtAgent { get; private set; }
    [SerializeField] private float _chaseRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _whatIsWall;
    [field: SerializeField] public EnemySO enemySO { get; private set; }
    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        BtAgent = GetComponent<BehaviorGraphAgent>();
        AnimCompo = GetComponentInChildren<Animator>();
        AgentCompo = GetComponent<NavMeshAgent>();
        //MovementCompo = GetComponentInChildren<PathMovement>();
        VisualCompo = GetComponentInChildren<EnemyRenderer>();
    }
    public bool CheckChaseRange()
    {
        return Physics2D.OverlapCircle(transform.position, _chaseRange, _playerMask);
    }
    public bool CheckAttackRange()
    {
        return Physics2D.OverlapCircle(transform.position, _attackRange, _playerMask);
    }
    public bool IsPlayerInLineOfSight()
    {
        if (target == null) return false;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        float dist = Vector2.Distance(transform.position, target.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dist, _whatIsWall);

        return hit.collider == null;
    }
    private void LateUpdate()
    {
        if(VisualCompo != null)
            VisualCompo.Filp(target.position - transform.position);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
#endif
    public void Initialize(Agent agent)
    {
        
    }
}
