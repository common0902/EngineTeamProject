using System;
using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class AoeAttack : MonoBehaviour
    {
        private Boss _boss;
        private float _damagePerTick;
        private float _tickInterval;
        private float _duration;
        private LayerMask _targetLayer;

        [SerializeField] private BoxCollider2D _boxCollider;

        public void Initialize(Boss boss, float damagePerTick, float tickInterval, float duration, LayerMask targetLayer)
        {
            _boss = boss;
            _damagePerTick = damagePerTick;
            _tickInterval = tickInterval;
            _duration = duration;
            _targetLayer = targetLayer;

            if (_boxCollider == null)
                _boxCollider = GetComponent<BoxCollider2D>();

            if (_boxCollider == null)
            {
                Debug.LogError("BoxCollider2D 가 없습니다.");
                return;
            }

            Debug.Log("Start");
            StartCoroutine(TickDamageRoutine());
        }

        private IEnumerator TickDamageRoutine()
        {
            float elapsed = 0f;

            while (elapsed < _duration)
            {
                DoDamage();

                elapsed += _tickInterval;
                yield return new WaitForSeconds(_tickInterval);
            }
        }
        private void DoDamage()
        {
            Vector2 center = _boxCollider.bounds.center;
            Vector2 size = _boxCollider.bounds.size;
            float angle = _boxCollider.transform.eulerAngles.z;

            Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, angle, _targetLayer);
                
            foreach (var hit in hits)
            {
                var health = hit.GetComponent<HealthSystem>();
                if (health != null)
                {
                    health.Damage(_damagePerTick);
                    Debug.Log($"[AoeAttack] Hit {hit.name}, Damage = {_damagePerTick}");
                }
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_boxCollider == null) return;

            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(transform.parent.position, _boxCollider.bounds.size);
        }
#endif
    }
}
