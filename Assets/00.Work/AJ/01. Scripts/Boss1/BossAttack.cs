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
        public bool IsBombAnimationEnd { get; private set; }
        private void Awake()
        {
            _bossAnimator = GetComponentInChildren<BossAnimator>();
            _boss = GetComponent<Boss>();
        }

        private void Start()
        {
            _bossAnimator.OnAttackTrigger += Attack;
            _bossAnimator.OnAttackEndTrigger += OnAttackEnd;
            _bossAnimator.OnAppearTrigger += OnAppearEnd;
            _bossAnimator.OnReturnToIdleTrigger += OnAppearEnd;
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
                    Debug.Log(_attackBehavior);
                    break;
                case BossStateType.AttackSummon:
                    _attackBehavior = new BossSummonAttackBehavior();  
                    break;
                case BossStateType.AttackRange:
                    _attackBehavior = new BossRangeAttackBehavior();  
                    break;
                case BossStateType.AttackBomb:
                    _attackBehavior = new BossBombAttackBehavior();
                    break;
                case BossStateType.AttackLaser:
                    _attackBehavior = new BossLaserAttackBehavior();
                    break;
                case BossStateType.AttackAOE:
                    _attackBehavior = new BossAoeAttackBehavior();
                    Debug.Log("dkdkd");
                    break;
                default:
                    Debug.LogError($"No Type: {_boss.CurrentType}");
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
        public void OnAppearEnd()
        {
            IsAnimationEnd = true;
        }

        private void OnDestroy()
        {
            if (_bossAnimator != null)
            {
                _bossAnimator.OnAttackTrigger -= Attack;
                _bossAnimator.OnAttackEndTrigger -= OnAttackEnd;
                _bossAnimator.OnAppearTrigger -= OnAppearEnd;   
            }
        }
    }
}