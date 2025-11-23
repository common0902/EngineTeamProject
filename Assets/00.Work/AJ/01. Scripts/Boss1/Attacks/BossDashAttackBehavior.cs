using System.Collections;
using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossDashAttackBehavior : IBossAttackBehavior
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
            float dashSpeed = 10f;
            float dashDuration = 0.5f;
            float elapsed = 0f;
            Debug.Log("dkdkdkd");
            SoundManager.Instance.PlaySound("BossDash");
            SoundManager.Instance.PlaySound("BossSword");
            while (elapsed < dashDuration)
            {
                Vector2 nextPos = (Vector2)_boss.transform.position + direction * dashSpeed * Time.deltaTime;
                Debug.Log(nextPos);
                Debug.Assert(Physics2D.OverlapCircle(nextPos, _boss.detectDistance, _boss.whatIsWall) == true, "Wall");
                if (Physics2D.OverlapCircle(nextPos, _boss.detectDistance, _boss.whatIsWall))
                {
                    _boss.RbCompo.linearVelocity = Vector2.zero;
                    break;
                }
                
                _boss.RbCompo.linearVelocity = direction * dashSpeed;
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            _boss.RbCompo.linearVelocity = Vector2.zero;
            
            yield return new WaitForSeconds(0.1f);

            if (_boss.CheckAttackRange())
                _targetHealth.Damage(_boss.Damage);

            OnAttackAnimationEnd();
        }

        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}