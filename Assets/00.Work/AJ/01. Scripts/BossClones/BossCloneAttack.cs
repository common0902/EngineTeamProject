using _00.Work.AJ._01._Scripts.BOSS;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        private BossAnimator _bossAnimator;
        private BossClone _boss;
        public bool isAnimationEnd = false;
        private void Awake()
        {
            _boss = GetComponent<BossClone>();
            _bossAnimator = GetComponentInChildren<BossAnimator>();
        }

        private void Start()
        {
            _bossAnimator.OnAttackTrigger += Attack;
            _bossAnimator.OnAttackEndTrigger += () => isAnimationEnd = true;
        }

        private void Attack()
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            Vector2 dir = _boss.Target.position - transform.position;
            bullet.GetComponent<Boss2.Bullet>().SetUp(_boss, dir.normalized, 5f);
        }
    }
}