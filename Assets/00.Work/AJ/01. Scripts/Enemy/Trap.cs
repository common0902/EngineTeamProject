using System.Collections;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject _rangeIndicatorPrefab;
    [SerializeField] private LayerMask playerMask;
    
    private EnemyTrapperData _data;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private bool _isActivated = false;
    private bool _isTriggered = false;
    
    private SpriteRenderer _rangeIndicator;
    
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
        if (_rangeIndicatorPrefab == null) return;
        GameObject rangeObj = Instantiate(_rangeIndicatorPrefab, transform);
        rangeObj.transform.localPosition = Vector3.zero;
        
        _rangeIndicator = rangeObj.GetComponent<SpriteRenderer>();
        
        if (_rangeIndicator != null)
        {
            _rangeIndicator.color = new Color(1f, 1f, 0f, 0.3f);
        }
    }
    private void UpdateRangeIndicator()
    {
        if (_rangeIndicator == null || _data == null) return;
        
        float diameter = _data.trapTriggerRadius * 2f;
        _rangeIndicator.transform.localScale = new Vector3(diameter, diameter, 1f);
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
            
            if (_rangeIndicator != null)
            {
                Color rangeColor = Color.Lerp(new Color(1f, 1f, 0f, 0.3f), new Color(1f, 0f, 0f, 0.8f), t);
                _rangeIndicator.color = rangeColor;
            }
            
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
            
            Color rangeColor = Color.Lerp(new Color(1f, 0f, 0f, 0.3f), new Color(1f, 0f, 0f, 0.7f), t);
            
            if (_rangeIndicator != null)
            {
                _rangeIndicator.color = rangeColor;
            }
            
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
        if (other.TryGetComponent(out Player player))
        {
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                _isTriggered = true;
                _isBlinking = false;
                TriggerTrap(playerHealth);
            }
        }
    }
    private void TriggerTrap(HealthSystem target)
    {
        StartCoroutine(ExplodeEffect(target));
    }
    private IEnumerator ExplodeEffect(HealthSystem target)
    {
        for (int i = 0; i < 3; i++)
        {
            _spriteRenderer.color = Color.red;
            if (_rangeIndicator != null)
            {
                _rangeIndicator.color = new Color(1f, 0f, 0f, 1f);
            }
            yield return new WaitForSeconds(0.05f);
            
            _spriteRenderer.color = Color.white;
            if (_rangeIndicator != null)
            {
                _rangeIndicator.color = new Color(1f, 1f, 1f, 1f);
            }
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
        
        if(Physics2D.OverlapCircle(transform.position, _data.trapTriggerRadius, playerMask))
            target.Damage(_data.trapDamage);
        
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
