using System;
using System.Collections;
using System.Collections.Generic;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class GoldDrop : MonoBehaviour, IPoolable
    {
        public string ItemName => itemName;
        [SerializeField] private string itemName;
        public GameObject GameObject => gameObject;
        
        [SerializeField] private float spreadTime = 0.3f;
        [SerializeField] private float speed = 5f;
        [SerializeField] private int goldAmount = 1;
        
        private Vector2 _target;
        private Rigidbody2D _rb;
        private bool _isFollow;
        private Coroutine _dropRoutine;
        private GoldSystem _goldSystem;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _isFollow = false;
        }

        public void Setup(Vector2 targetWorldPos, GoldSystem goldSystem, int goldAmount)
        {
            _target = targetWorldPos;
            _goldSystem = goldSystem;
            this.goldAmount = goldAmount;

            ResetPhysics();
            
            Vector2 randomForce = new Vector2(UnityEngine.Random.Range(-2.5f, 2.5f), UnityEngine.Random.Range(3f, 5f));
            _rb.AddForce(randomForce, ForceMode2D.Impulse);
            
            
            if (_dropRoutine != null)
            {
                StopCoroutine(_dropRoutine);
            }
            _dropRoutine = StartCoroutine(DropRoutine());
        }

        private IEnumerator DropRoutine()
        {
            _isFollow = false;
            _rb.gravityScale = 1f;
            yield return new WaitForSeconds(spreadTime);
            
            _rb.gravityScale = 0f;
            _rb.linearVelocity = Vector2.zero;
            _isFollow = true;
        }
        
        private void Update()
        {
            if (!_isFollow) return;
            
            Vector2 currentPos = transform.position;
            Vector2 newPos = Vector2.MoveTowards(currentPos, _target, speed * Time.deltaTime);
            transform.position = newPos;
            
            if (Vector2.Distance(newPos, _target) <= 0.1f)
            {
                ReachTarget();
            }
        }

        private void ReachTarget()
        {
            _goldSystem?.GetGold(goldAmount);
            PoolManager.Instance.Push(this);
        }

        private void ResetPhysics()
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        public void ResetItem()
        {
            if (_dropRoutine != null)
            {
                StopCoroutine(_dropRoutine);
                _dropRoutine = null;
            }

            _isFollow = false;
            _goldSystem = null;
            ResetPhysics();
            _rb.gravityScale = 1f;
        }

    }
}