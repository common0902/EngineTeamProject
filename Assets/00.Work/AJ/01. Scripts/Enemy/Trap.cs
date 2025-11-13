using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private EnemyTrapperData _data;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private bool _isActivated = false;
    private bool _isTriggered = false;
    
    private LineRenderer _rangeIndicator;
    private int _circleSegments = 50;
    
    private float _blinkInterval = 0.5f;
    private bool _isBlinking = false;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        
        CreateRangeIndicator();
    }

    public void Initialize(EnemyTrapperData data)
    {
        _data = data;
        _collider.radius = data.trapTriggerRadius;
        
        StartCoroutine(ActivationSequence());
        StartCoroutine(LifetimeRoutine());
    }
    private void CreateRangeIndicator()
    {
        GameObject rangeObj = new GameObject("RangeIndicator");
        rangeObj.transform.SetParent(transform);
        rangeObj.transform.localPosition = Vector3.zero;
        
        _rangeIndicator = rangeObj.AddComponent<LineRenderer>();
        _rangeIndicator.positionCount = _circleSegments + 1;
        _rangeIndicator.useWorldSpace = false;
        _rangeIndicator.startWidth = 0.05f;
        _rangeIndicator.endWidth = 0.05f;
        _rangeIndicator.loop = true;
        
        _rangeIndicator.material = new Material(Shader.Find("Sprites/Default"));
        _rangeIndicator.startColor = new Color(1f, 1f, 0f, 0.3f); // 반투명 노란색
        _rangeIndicator.endColor = new Color(1f, 1f, 0f, 0.3f);
        
        _rangeIndicator.sortingLayerName = "Default";
        _rangeIndicator.sortingOrder = 5;
    }
    private void UpdateRangeIndicator()
    {
        if (_rangeIndicator == null || _data == null) return;
        
        float radius = _data.trapTriggerRadius;
        float angle = 0f;
        float angleStep = 360f / _circleSegments;
        
        for (int i = 0; i <= _circleSegments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            
            _rangeIndicator.SetPosition(i, new Vector3(x, y, 0));
            angle += angleStep;
        }
    }
    private IEnumerator ActivationSequence()
    {
        float elapsed = 0f;
        Color startColor = _spriteRenderer.color;
        
        UpdateRangeIndicator();
        
        while (elapsed < _data.trapActivationDelay)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / _data.trapActivationDelay;
            
            float speed = Mathf.Lerp(2f, 8f, progress);
            float t = Mathf.PingPong(Time.time * speed, 1f);
            
            _spriteRenderer.color = Color.Lerp(startColor, _data.trapWarningColor, t);
            
            Color rangeColor = Color.Lerp(new Color(1f, 1f, 0f, 0.3f), new Color(1f, 0f, 0f, 0.8f), t);
            _rangeIndicator.startColor = rangeColor;
            _rangeIndicator.endColor = rangeColor;
            
            yield return null;
        }
        
        _spriteRenderer.color = Color.white;
        _isActivated = true;
        
        StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        _isBlinking = true;
        
        while (_isBlinking && !_isTriggered)
        {
            float t = Mathf.PingPong(Time.time * 1.5f, 1f);
            
            Color rangeColor = Color.Lerp(
                new Color(1f, 0f, 0f, 0.3f),  // 반투명 빨강
                new Color(1f, 0f, 0f, 0.7f),  // 진한 빨강
                t
            );
            _rangeIndicator.startColor = rangeColor;
            _rangeIndicator.endColor = rangeColor;
            
            yield return null;
        }
    }
    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(_data.trapLifetime);
        
        if (!_isTriggered)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActivated || _isTriggered)
            return;
        
        HealthSystem playerHealth = other.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            _isTriggered = true;
            _isBlinking = false;
            TriggerTrap(playerHealth);
        }
    }
    private void TriggerTrap(HealthSystem target)
    {
        target.Damage(_data.trapDamage);
        
        StartCoroutine(ExplodeEffect());
    }
    private IEnumerator ExplodeEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            _spriteRenderer.color = Color.red;
            _rangeIndicator.startColor = new Color(1f, 0f, 0f, 1f);
            _rangeIndicator.endColor = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.05f);
            
            _spriteRenderer.color = Color.white;
            _rangeIndicator.startColor = new Color(1f, 1f, 1f, 1f);
            _rangeIndicator.endColor = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.05f);
        }
        
        if (_data.placeTrapVFX != null)
        {
            ParticleSystem explosion = Instantiate(
                _data.placeTrapVFX,
                transform.position,
                Quaternion.identity
            );
            explosion.Play();
            Destroy(explosion.gameObject, 2f);
        }
        
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_data != null)
        {
            Gizmos.color = _isActivated ? Color.red : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _data.trapTriggerRadius);
        }
    }
#endif
}
