using _00.Work.AJ._01._Scripts.BOSS;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BossClones
{
    public class BossCloneHit : MonoBehaviour
    {
        private BossAnimator _bossAnimator;
        public bool isAnimationEnd = false;
        private BossClone _boss;
        private void Awake()
        {
            _bossAnimator = GetComponentInChildren<BossAnimator>();
            _boss = GetComponent<BossClone>();
        }
        private void Start()
        {
            _bossAnimator.OnHitEndTrigger += HandleHitEnd;
            _boss.HealthCompo.OnHealthChanged += HandleHealthChanged;
            _boss.HealthCompo.OnDead += HandleDead;
            _bossAnimator.OnDeathEndTrigger += HandleDeathEnd;
        }
        private void HandleHitEnd() => isAnimationEnd = true;
        private void HandleHealthChanged(float health, float maxHealth) => _boss.IsHit = true;
        private void HandleDead() => _boss.IsDead = true;
        private void HandleDeathEnd() => isAnimationEnd = true;
        private void OnDestroy()
        {
            if (_bossAnimator != null)
            {
                _bossAnimator.OnHitEndTrigger -= HandleHitEnd;
                _bossAnimator.OnDeathEndTrigger -= HandleDeathEnd;
            }
        
            if (_boss?.HealthCompo != null)
            {
                _boss.HealthCompo.OnHealthChanged -= HandleHealthChanged;
                _boss.HealthCompo.OnDead -= HandleDead;
            }
        }
    }
}