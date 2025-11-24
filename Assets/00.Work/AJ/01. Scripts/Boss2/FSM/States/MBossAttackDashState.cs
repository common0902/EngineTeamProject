using System.Collections;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.Attacks
{
    public class MBossAttackDashState : IMBossAttackBehaviour
    {
        public void Initialize(MiddleBoss boss)
        {
            
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public void OnAttackAnimationEnd()
        {
            throw new System.NotImplementedException();
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}