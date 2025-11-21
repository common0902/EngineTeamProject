using System;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class BossAnimator : MonoBehaviour
    {
        public Action OnAttackStartTrigger;
        public Action OnAttackTrigger;
        public Action OnAttackEndTrigger;
        public Action OnHitEndTrigger;
        public Action OnDeathEndTrigger;
        public Action OnVanishTrigger;
        public Action OnAppearTrigger;
        public Action OnReturnToIdleTrigger;
        public void AttackStart()
        {
            OnAttackStartTrigger?.Invoke();
        }
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
        public void Vanish()
        {
            OnVanishTrigger?.Invoke();
        }

        public void Appear()
        {
            OnAppearTrigger?.Invoke();
        }

        public void ReturnToIdle()
        {
            OnReturnToIdleTrigger?.Invoke();
        }
    }
}