using System.Collections;
using _00.Work.AJ._01._Scripts.BOSS;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        private BossCloneAnimator _bossAnimator;
        private BossClone _boss;
        public bool isAnimationEnd = false;
        public Coroutine _attackCoroutine;
        private void Awake()
        {
            _boss = GetComponent<BossClone>();
            _bossAnimator = GetComponentInChildren<BossCloneAnimator>();
        }

        private void Start()
        {
            _bossAnimator.OnAttackTrigger += Attack;
            _bossAnimator.OnAttackEndTrigger += () => isAnimationEnd = true;
        }

        private void Attack()
        {
            if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
            _attackCoroutine = StartCoroutine(InstantiateBullet());
        }

        private IEnumerator InstantiateBullet()
        {
            for (int i = 0; i < 3; i++)
            {
                var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
                Vector2 dir = _boss.Target.position - transform.position;
                bullet.GetComponent<Boss2.Bullet>().SetUp(_boss, dir.normalized, 5f);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}