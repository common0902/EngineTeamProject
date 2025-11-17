using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossHit : MonoBehaviour
    {
        private BossAnimator _bossAnimator;
        public bool isAnimationEnd = false;
        private Boss _boss;
        private void Awake()
        {
            _bossAnimator = GetComponentInChildren<BossAnimator>();
            _boss = GetComponent<Boss>();
        }
        private void Start()
        {
            _bossAnimator.OnHitEndTrigger += () => isAnimationEnd = true;
            _boss.HealthCompo.OnHealthChanged += (float health, float maxHealth) => _boss.IsHit = true;
            _boss.HealthCompo.OnDead += () => _boss.IsDead = true;
            _bossAnimator.OnDeathEndTrigger += () => Destroy(_boss.gameObject, 1f);
        }
    }
}