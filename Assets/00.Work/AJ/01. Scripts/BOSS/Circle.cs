using System;
using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class Circle : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask playerMask;
        private Coroutine _damageCoroutine;
        private bool _isPlayerInside;
        private Boss _boss;
        private float _damageMultiplier;
        private float _tickInterval;

        public void Init(Boss boss, float damageMultiplier, float tickInterval)
        {
            _boss = boss;
            _damageMultiplier = damageMultiplier;
            _tickInterval = tickInterval;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player) && !_isPlayerInside)
            {
                _isPlayerInside = true;
                
                if (_damageCoroutine != null)
                {
                    StopCoroutine(_damageCoroutine);
                }
                
                _damageCoroutine = StartCoroutine(DamageOverTime(other.gameObject));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player) && _isPlayerInside)
            {
                _isPlayerInside = false;
                
                if (_damageCoroutine != null)
                {
                    StopCoroutine(_damageCoroutine);
                    _damageCoroutine = null;
                }
            }
        }
        
        private IEnumerator DamageOverTime(GameObject target)
        {
            while (_isPlayerInside && target != null)
            {
                float damage = _boss.Damage * _damageMultiplier;
                Player.Instance.PlayerHealthSystemCompo.Damage(damage);
                
                yield return new WaitForSeconds(_tickInterval);
            }
            
            _damageCoroutine = null;
        }
        private void OnDisable()
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
            _isPlayerInside = false;
        }
        private void OnDestroy()
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
