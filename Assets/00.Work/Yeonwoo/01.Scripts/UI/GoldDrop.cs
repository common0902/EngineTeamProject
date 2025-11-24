using System.Collections;
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
    
        private Transform _targetTransform;
        private Rigidbody2D _rb;
        private bool _isFollow;
        private Coroutine _dropRoutine;
        private GoldSystem _goldSystem;
    
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        public void Setup(Transform targetTransform, GoldSystem goldSystem, int goldAmount)
        {
            _targetTransform = targetTransform;
            _goldSystem = goldSystem;
            this.goldAmount = goldAmount;

            ResetPhysics();
        
            Vector2 randomForce = new Vector2(
                Random.Range(-2.5f, 2.5f),
                Random.Range(3f, 5f));
        
            _rb.AddForce(randomForce, ForceMode2D.Impulse);

            if (_dropRoutine != null)
                StopCoroutine(_dropRoutine);

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
            if (_targetTransform == null) return;

            Vector2 currentPos = transform.position;
            Vector2 target = _targetTransform.position; // 계속 타겟 잡게함

            Vector2 newPos = Vector2.MoveTowards(currentPos, target, speed * Time.deltaTime);
            transform.position = newPos;

            if (Vector2.Distance(newPos, target) <= 0.1f)
            {
                ReachTarget();
            }
        }

        private void ReachTarget()
        {
            _goldSystem?.GetGold(goldAmount);
            PoolManager2.Instance.Push(this);
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
            _targetTransform = null;
            _goldSystem = null;

            ResetPhysics();
            _rb.gravityScale = 1f;
        }
    }
}
