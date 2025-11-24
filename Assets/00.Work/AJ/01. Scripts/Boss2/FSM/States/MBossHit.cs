using _00.Work.AJ._01._Scripts.BOSS;
using _00.Work.PMS._01.Scripts;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MiddleBossHit : MonoBehaviour
    {
        private BossAnimator _bossAnimator;
        public bool isAnimationEnd = false;
        private MiddleBoss _boss;
        private MiddleBossRoom _parentBossRoom;
        private void Awake()
        {
            _bossAnimator = GetComponentInChildren<BossAnimator>();
            _boss = GetComponent<MiddleBoss>();
        }
        private void OnEnable()
        {
            _bossAnimator.OnHitEndTrigger += HandleHitEnd;
            _boss.HealthCompo.OnHealthChanged += HandleHealthChanged;
            _boss.HealthCompo.OnDead += HandleDead;
            _bossAnimator.OnDeathEndTrigger += HandleDeathEnd;
        }
        private void HandleHitEnd() => isAnimationEnd = true;
        private void HandleHealthChanged(float health, float maxHealth)
        {
            _boss.IsHit = true;
            SoundManager.Instance.PlaySound("Hit");
        }

        private void HandleDead()
        {
            _parentBossRoom.OnEnemyDied();
            _boss.IsDead = true;
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