using System;
using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class AoeAttack : MonoBehaviour
    {
        private Boss _boss;
        private float _tickInterval;
        private float _damageMultiplier;
        private LayerMask _targetLayer;
        private Coroutine _damageCoroutine;
        private bool _isPlayerInside = false; // 플레이어 상태 추적

        public void Initialize(Boss boss, float damageMultiplier, float tickInterval, LayerMask targetLayer)
        {
            _boss = boss;
            _tickInterval = tickInterval;
            _damageMultiplier = damageMultiplier;
            _targetLayer = targetLayer;
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
    }
}
