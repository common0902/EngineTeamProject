using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossHit : MonoBehaviour
    {
        private BossAnimator _bossAnimator;
        public bool isAnimationEnd = false;
        private Boss _boss;
        private BossRoom _bossRoom;
        private void Awake()
        {
            _bossAnimator = GetComponentInChildren<BossAnimator>();
            _boss = GetComponent<Boss>();
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
        private void HandleDead()
        {
            _bossRoom = GetComponentInParent<BossRoom>();
            _boss.IsDead = true;
            _bossRoom.OnEnemyDied();
        }
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