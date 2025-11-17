using _00.Work.AJ._01._Scripts.BOSS.Attacks;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class BossAttack : MonoBehaviour
    {
        private BossAnimator _bossAnimator;
        private Boss _boss;
        private IBossAttackBehavior _attackBehavior;
        public bool IsAnimationEnd { get; set; }
        private void Awake()
        {
            _bossAnimator = GetComponentInChildren<BossAnimator>();
            _boss = GetComponent<Boss>();
        }

        private void Start()
        {
            _bossAnimator.OnAttackTrigger += Attack;
            _bossAnimator.OnAttackEndTrigger += OnAttackEnd;
        }

        private void InitializeAttackBehavior()
        {
            switch (_boss.CurrentType)
            {
                case BossStateType.AttackMelee:
                    _attackBehavior = new BossMeleeAttackBehavior();
                    break;
                case BossStateType.AttackDash:
                    _attackBehavior = new BossDashAttackBehavior();
                    break;
                case BossStateType.AttackSummon:
                    _attackBehavior = new BossSummonAttackBehavior();  
                    break;
                case BossStateType.AttackRange:
                    _attackBehavior = new BossRangeAttackBehavior();  
                    break;
                default:
                    Debug.LogError($"지원하지 않는 공격 타입: {_boss.CurrentType}");
                    _attackBehavior = null;
                    break;
            }
            
            _attackBehavior?.Initialize(_boss);
        }

        private void Attack()
        {
            InitializeAttackBehavior();
            
            if (_attackBehavior == null) Debug.Log("dkdkdkdkdd");
            Vector2 dir = (_boss.Target.position - _boss.transform.position).normalized;
            StartCoroutine(_attackBehavior?.ExecuteAttack(dir));
        }
        public void OnAttackEnd()
        {
            IsAnimationEnd = true;
            _attackBehavior?.OnAttackAnimationEnd();
        }

        private void OnDestroy()
        {
            if (_bossAnimator != null)
            {
                _bossAnimator.OnAttackTrigger -= Attack;
                _bossAnimator.OnAttackEndTrigger -= OnAttackEnd;
            }
        }
    }
}