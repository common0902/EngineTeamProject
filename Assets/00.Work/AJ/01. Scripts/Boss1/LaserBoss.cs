using System;
using System.Collections;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class LaserBoss : MonoBehaviour
    {
        private Boss _boss;
        private bool _isPlayerInside;
        private Coroutine _damageCoroutine;
        private float _damageMultiplier;
        private float _tickInterval;
        private LayerMask _targetLayer;

        public void Init(Boss boss, float damageMultiplier, float tickInterval, LayerMask targetLayer)
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

                Debug.Log("Start");
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
                Debug.Log("Delay");
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
