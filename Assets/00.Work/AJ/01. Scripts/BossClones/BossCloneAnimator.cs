using System;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneAnimator : MonoBehaviour
    {
        public Action OnAttackTrigger;
        public Action OnAttackEndTrigger;
        public Action OnHitEndTrigger;
        public Action OnDeathEndTrigger;

        public void Attack()
        {
            OnAttackTrigger?.Invoke();
        }

        public void AttackEnd()
        {
            OnAttackEndTrigger?.Invoke();
        }

        public void HitEnd()
        {
            OnHitEndTrigger?.Invoke();
        }
        public void DeathEnd()
        {
            OnDeathEndTrigger?.Invoke();
        }
        
    }
}
