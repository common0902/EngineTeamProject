using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public abstract class AbstractSkills : MonoBehaviour
    {
        [SerializeField] private float range = 2f;
        private Transform _playerPos;
        private bool _isPlayerInRange = false;
        
        protected virtual void OnEnable()
        {
            GameObject player = GameObject.Find("Player"); // 에디터 이름으로 찾는거라 이름은 Player로 계속 유지
            if (player != null)
            {
                _playerPos = player.transform;
            }
            else if (player == null)
                Debug.LogError("Not found player");
            
            if (_playerPos == null)
            {
                Debug.LogWarning($"[{nameof(AbstractSkills)}] Player Transform not found. Make sure there is a GameObject named 'Player' or assign player transform some other way.");
            }
        }
        
        protected virtual void Update()
        {
            if (_playerPos == null) return; 
            Interaction();
        }

        private void Interaction()
        {
            float distance = Vector3.Distance(transform.position, _playerPos.position);

            if (distance <= range)
            {
                if (!_isPlayerInRange)
                {
                    Debug.Log("플레이어 인");
                    OnPlayerEnterRange();
                    _isPlayerInRange = true;
                }
            }
            else
            {
                if (_isPlayerInRange)
                {
                    Debug.Log("플레이어 아웃");
                    OnPlayerExitRange();
                    _isPlayerInRange = false;
                }
            }
        }
        
        protected virtual void OnPlayerEnterRange() { }
        protected virtual void OnPlayerExitRange() { }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}