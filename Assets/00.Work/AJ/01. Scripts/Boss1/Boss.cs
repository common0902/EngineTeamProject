using System;
using System.Collections.Generic;
using _00.Work.AJ._01._Scripts.Boss1.FSM.States;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class Boss : MonoBehaviour
    {
        public Transform Target { get; private set; }
        [field:SerializeField]public Transform FirePos { get; private set; }
        [SerializeField] public LayerMask playerMask;
        [SerializeField] public LayerMask whatIsWall;
        [field:SerializeField] public float Speed { get; set; } = 1f;
        [field:SerializeField] public float ChaseRange { get; private set; }
        [field:SerializeField] public float AttackRange { get; private set; }
        [field:SerializeField] public List<BossStateType> Patterns { get; set; }= new() { BossStateType.AttackMelee, BossStateType.AttackDash, BossStateType.AttackSummon, BossStateType.AttackRange };
        [field:SerializeField] public BossStateType CurrentType { get; set; } = BossStateType.AttackMelee;
        public GameObject[] summonPrefabs;
        public GameObject white;
        public GameObject laserPoint;
        public GameObject laserPrefab;
        public float detectDistance = 5f;
        public bool IsDead { get; set; }
        public bool IsHit { get; set; }
        [field:SerializeField]public int CurrentSummonCount { get; set; }= 0;
        [field: SerializeField] public Transform CenterPos { get; private set; }
        [field: SerializeField] public GameObject Bomb { get; private set; }
        [field: SerializeField] public SpriteRenderer RangeSprite { get; set; }
        [field: SerializeField] public GameObject AoeSprite { get; private set; }
        [field: SerializeField] public GameObject Aoe { get; private set; }
        public BossDeathState DeathState { get; set; }
        #region Components
        [field:SerializeField]public WayPoints WayPoints { get; private set; }
        public Animator AnimCompo { get; private set; }
        public BossRenderer VisualCompo { get; private set; }
        public Rigidbody2D RbCompo { get; private set; }
        public Collider2D ColliderCompo { get; private set; }
        public HealthSystem HealthCompo { get; private set; }
        public bool Phase3Executed { get; set; }
        public bool Phase2Unlocked { get; set; }
        public bool HasStarted { get; set; }
        [field:SerializeField]public float Damage { get; set; }
        [field:SerializeField]public GameObject BulletPrefab { get; set; }
        public Transform PreviousPos { get; private set; }
        #endregion
        private void Awake()
        {
            AnimCompo = GetComponentInChildren<Animator>();
            VisualCompo = GetComponentInChildren<BossRenderer>();
            VisualCompo.Init(this);
            RbCompo = GetComponent<Rigidbody2D>();
            ColliderCompo = GetComponent<Collider2D>();
            HealthCompo = GetComponent<HealthSystem>();
            //Target = FindAnyObjectByType<Player>().transform;
            if (Target == null)
            {
                Target = Player.Instance.transform;
            }
            if (WayPoints == null)
                WayPoints = GetComponentInParent<WayPoints>();
            Patterns = new() { BossStateType.AttackMelee, BossStateType.AttackDash, BossStateType.AttackSummon, BossStateType.AttackRange };
            if (CenterPos == null) CenterPos = transform.parent.Find("CenterPos");
        }

        public bool CheckChaseRange()
        {
            return Physics2D.OverlapCircle(transform.position, ChaseRange, playerMask);
        }

        public bool CheckAttackRange()
        {
            return Physics2D.OverlapCircle(transform.position, AttackRange, playerMask);
        }

        private void LateUpdate()
        {
            VisualCompo.Flip(Target.position - transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ChaseRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, detectDistance);
        }
    }
}