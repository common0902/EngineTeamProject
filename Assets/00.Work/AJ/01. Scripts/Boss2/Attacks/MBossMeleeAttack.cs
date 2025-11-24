using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.Attacks
{
    public class MBossMeleeAttack : IMBossAttackBehaviour
    {
        private MiddleBoss _boss;
        private HealthSystem _targetHealth;
        public void Initialize(MiddleBoss boss)
        {
            _boss = boss;
            _targetHealth = boss.Target.GetComponent<HealthSystem>();
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            if (_boss.CheckDamageRange())
            {
                _targetHealth.Damage(_boss.Damage);
            }
            yield return null;
        }

        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}