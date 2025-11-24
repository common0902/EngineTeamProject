using System;
using _00.Work.AJ._01._Scripts.BOSS;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossClone : MonoBehaviour
    {
        public Rigidbody2D RbCompo { get; private set; }
        [field:SerializeField] public float Speed { get; set; }
        [field:SerializeField]public float Damage { get; private set; }
        public Transform Target { get; set; }
        [field:SerializeField]public float AttackRange { get; private set; }
        [field:SerializeField]public float ChaseRange { get; private set; }
        [SerializeField] private LayerMask playerMask;
        public Animator AnimCompo { get; private set; }
        public BossRenderer VisualCompo { get; private set; }
        public bool IsDead { get; set; }
        public bool IsHit { get; set; }
        public HealthSystem HealthCompo { get; set; }

        private void Awake()
        {
            AnimCompo = GetComponentInChildren<Animator>();
            VisualCompo = GetComponentInChildren<BossRenderer>();
            VisualCompo.Init(this);
            RbCompo = GetComponent<Rigidbody2D>();
            HealthCompo = GetComponent<HealthSystem>();
            Target = FindAnyObjectByType<Player>().transform;
            
        }

        public bool CheckAttackRange()
        {
            return Physics2D.OverlapCircle(transform.position, AttackRange, playerMask);
        }
        public bool CheckChaseRange()
        {
            return Physics2D.OverlapCircle(transform.position, ChaseRange, playerMask);
        }

        private void LateUpdate()
        {
            VisualCompo.Flip(Target.position - transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ChaseRange);
        }
    }
}
