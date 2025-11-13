using _00.Work.SYH._02Script.ETG;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private EnemySO enemySo;
    private bool _isParabola;
    private Vector2 _targetPosition;
    private float _arcHeight = 2f; 
    private float _travelTime;
    private float _elapsedTime;
    private Vector2 _startPosition;
    private float _speed;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();        
    }

    public void SetUp(Vector2 dir, Enemy enemy, float speed)
    {
        enemySo = enemy.enemySO;
        _isParabola = enemySo.rangedData.bulletData.parabola;
        _speed = enemySo.rangedData.bulletData.bulletSpeed;

        if (_isParabola)
        {
            _rb.gravityScale = 0; 
            _startPosition = transform.position;
            
            _targetPosition = enemy.target.position;

            _travelTime = enemySo.rangedData.bulletData.arcTime;
            _elapsedTime = 0f;
        }
        else
        {
            _rb.linearVelocity = dir * _speed;
        }
        StartCoroutine(LifeTimeCoroutine(enemySo.rangedData.bulletData.lifeTime));
    }
    
    private void Update()
    {
        if (_isParabola)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / _travelTime;
            
            if (t <= 1f)
            {
                Vector2 newPosition = CalculateParabolicPosition(t);
                
                _rb.MovePosition(newPosition);
                
                Vector2 moveDir = newPosition - (Vector2)transform.position;
                float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    private void FixedUpdate()
    {
        if(enemySo.rangedData.bulletData.multibulletShoot)
            _rb.linearVelocity = transform.right * _speed;
    }

    private Vector2 CalculateParabolicPosition(float t)
    {
        Vector2 linearPosition = Vector2.Lerp(_startPosition, _targetPosition, t);

        float height = _arcHeight * Mathf.Sin(Mathf.PI * t);
        linearPosition.y += height;

        return linearPosition;
    }
    private IEnumerator LifeTimeCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem hp))
        {
            hp.Damage(enemySo.damage);
            Destroy(gameObject);    
        }
        else
            Destroy(gameObject);   
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!_isParabola) return;
        if (_startPosition == Vector2.zero || _targetPosition == Vector2.zero) return;

        Gizmos.color = Color.yellow;
        const int resolution = 30; 

        Vector2 prevPos = _startPosition;

        for (int i = 1; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector2 pos = CalculateParabolicPosition(t);
            Gizmos.DrawLine(prevPos, pos);
            prevPos = pos;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_startPosition, 0.05f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_targetPosition, 0.05f);
    }
#endif
}
