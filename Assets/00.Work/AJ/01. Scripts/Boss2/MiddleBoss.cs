using System.Collections.Generic;
using _00.Work.AJ._01._Scripts.BOSS;
using _00.Work.AJ._01._Scripts.Boss2.FSM;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2
{
    public class MiddleBoss : MonoBehaviour
    {
        public Transform Target { get; private set; }
        [field:SerializeField] public Transform FirePos { get; private set; } 
        [SerializeField] private LayerMask playerMask;
        [SerializeField] public LayerMask whatIsWall;
        [field:SerializeField] public float Speed { get; set; } = 1f;
        [field:SerializeField] public float AttackRange { get; private set; }
        [field:SerializeField]public float Damage { get; set; }
        [field:SerializeField]public GameObject BulletPrefabs { get; set; }
        private bool _canFlip = true;
        public bool IsDead { get; set; } = false;
        [field:SerializeField]public bool IsHit { get; set; } = false;
        [field:SerializeField]public Vector2 offSetDamageRange { get; set; }
        [field:SerializeField] public float DamageRange { get; set; }
        
        public bool HasStarted { get; set; } = false;
        #region Components
        [field:SerializeField]public WayPoints WayPoints { get; private set; }
        [field:SerializeField]public Transform CenterPos { get; private set; }
        public Rigidbody2D RbCompo { get; private set; }
        public CapsuleCollider2D ColliderCompo { get; private set; }
        public HealthSystem HealthCompo { get; private set; }
        public Animator AnimCompo { get; private set; }
        public BossRenderer VisualCompo { get; private set; }
        [field:SerializeField]public MiddleBossStateType CurrentType { get; set; }
        [field:SerializeField]public List<MiddleBossStateType> Patterns { get; set; }
        public bool Phase2Activated { get; set; }
        [field:SerializeField]public List<GameObject> Bosses { get; private set; }
        #endregion
    
        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
            ColliderCompo = GetComponent<CapsuleCollider2D>();
            HealthCompo = GetComponent<HealthSystem>();
            AnimCompo = GetComponentInChildren<Animator>();
            VisualCompo = GetComponentInChildren<BossRenderer>();
            VisualCompo.Init(this);
            WayPoints = GetComponentInParent<WayPoints>();
            
            if (CenterPos == null) CenterPos = WayPoints.transform.Find("CenterPos");
            Patterns = new() {MiddleBossStateType.Attack, MiddleBossStateType.Range, MiddleBossStateType.CircleRange };
            HasStarted = false;
        }
        private void OnEnable()
        {
            Target = Player.Instance.transform;
            Debug.Log(Target);
            HealthCompo.OnHealthChanged += CheckPhase;
        }

        private void OnDisable()
        {
            HealthCompo.OnHealthChanged -= CheckPhase;
        }

        public bool CheckAttackRange()
        {
            return Physics2D.OverlapCircle(transform.position, AttackRange, playerMask);
        }

        public bool CheckDamageRange()
        {
            return Physics2D.OverlapCircle((Vector2)transform.position + offSetDamageRange, DamageRange, playerMask);
        }


        private void LateUpdate()
        {
            if (_canFlip)
                VisualCompo.Flip(Target.position - transform.position);
        }
        

        private void CheckPhase(float currentHealth, float maxHealth)
        {
            float healthPercent = currentHealth / maxHealth;
    
            if (healthPercent <= 0.5f && !Phase2Activated)
            {
                Phase2Activated = true;
                AddDashPattern();
            }
        }
        private void AddDashPattern()
        {
            if (!Patterns.Contains(MiddleBossStateType.Dash))
            {
                Patterns.Add(MiddleBossStateType.Dash);
            }
        }
        public bool CanFilp(bool value) => _canFlip =  value;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere((Vector2)transform.position + offSetDamageRange, DamageRange);
        }
    }
}
