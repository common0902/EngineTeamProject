using _00.Work.AJ._01._Scripts.BOSS;
using _00.Work.AJ._01._Scripts.Boss2.FSM;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.Attacks
{
    public class MiddleBossAttack : MonoBehaviour
    {
        private BossAnimator _bossAnimator;
        private MiddleBoss _boss;
        private IMBossAttackBehaviour _attackBehavior;
        public bool IsAnimationEnd { get; set; }
        private Coroutine _currentAttackRoutine;

        private void Awake()
        {
            _bossAnimator = GetComponentInChildren<BossAnimator>();
            _boss = GetComponent<MiddleBoss>();
        }
        private void Start()
        {
            _bossAnimator.OnAttackTrigger += Attack;
            _bossAnimator.OnAttackEndTrigger += OnAttackEnd;
            _bossAnimator.OnAppearTrigger += OnAppearEnd;
            _bossAnimator.OnVanishTrigger += OnVanishEnd;
            _bossAnimator.OnAttackStartTrigger += OnDashBeforeEnd;
        }

        private void OnVanishEnd()
        {
            IsAnimationEnd = true;
            _boss.GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,0);
        }

        private void InitializeAttackBehaviour()
        {
            switch (_boss.CurrentType)
            {
                case MiddleBossStateType.Attack:
                    _attackBehavior = new MBossMeleeAttack();
                    break;
                case MiddleBossStateType.Range:
                    _attackBehavior = new MBossRangeAttack();
                    break;
                case MiddleBossStateType.CircleRange:
                    _attackBehavior = new MBossCircleRangeAttack();
                    break;
                case MiddleBossStateType.Appear:
                    _attackBehavior = new MBossAppearAttack();
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
            if (_currentAttackRoutine != null)
            {
                StopCoroutine(_currentAttackRoutine);
                _currentAttackRoutine = null;
            }

            InitializeAttackBehaviour();
            if (_attackBehavior == null) Debug.Log("dkdkdkdkdd");
            Vector2 dir = (_boss.Target.position - _boss.transform.position).normalized;
            _currentAttackRoutine = StartCoroutine(_attackBehavior?.ExecuteAttack(dir));
        }
        public void OnAttackEnd()
        {
            if (_currentAttackRoutine != null)
            {
                StopCoroutine(_currentAttackRoutine);
                _currentAttackRoutine = null;
            }
            
            IsAnimationEnd = true;
            _attackBehavior?.OnAttackAnimationEnd();
        }
        public void OnAppearEnd()
        {
            IsAnimationEnd = true;  
        }

        public void OnDashBeforeEnd()
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