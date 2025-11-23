using System.Collections;
using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossMeleeAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss; 
        private HealthSystem _targetHealth;
        public void Initialize(Boss boss)
        {
            _boss = boss;
            _targetHealth = boss.Target.GetComponent<HealthSystem>();
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            SoundManager.Instance.PlaySound("BossSword");
            if (_boss.CheckAttackRange())
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