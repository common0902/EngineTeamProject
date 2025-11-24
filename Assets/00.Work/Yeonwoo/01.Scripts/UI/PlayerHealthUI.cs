using System;
using _00.Work.SYH._02Script.ETG;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private RectTransform healthBarTransform;
        [Header("Tween Settings")]
        [SerializeField] private float healthBarTweenDuration = 0.3f;
        [SerializeField] private Ease healthBarEase = Ease.OutCubic;

        private HealthSystem _healthSystem;

        private IHealth _health;
        private Slider _slider;
        private CanvasGroup _canvasGroup;
        
        private Vector3 _originPosition;
        private Tween _shakeTween;
        private Tween _healthBarTween;
        
        private TextMeshProUGUI _healthText;
        [SerializeField] private Image warningImage;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            if (!_slider)
                Debug.LogError("slider is null");

            _healthSystem = Player.Instance.GetComponent<HealthSystem>();
            if (!_healthSystem)
                Debug.LogError("healthSystem is null");
            
            _healthText = GetComponentInChildren<TextMeshProUGUI>();
            if (!_healthText)
                Debug.LogError("healthText is null");
            
            _canvasGroup = GetComponent<CanvasGroup>();
            if (!_canvasGroup)
                Debug.LogError("canvasGroup is null");
        }
        
        private void Start()
        {
            if (_healthSystem is IHealth health)
            {
                _health = health;
                _slider.maxValue = _health.MaxHealth;
                _slider.value = _health.Health;
                
                _healthText.text = $"{_health.Health:F0} / {_health.MaxHealth:F0}";
                
                _health.OnHealthChanged += UpdateHealthUI;
                _health.OnDead += DeadMotion;
                _health.OnLowHealth += WaringMotion;
                _health.OnRecoverHealth += RecoverMotion;
            }
            else
            {
                Debug.LogError("HealthUI: Assign an IHealth implementation to the healthSource.");
            }
            _originPosition = healthBarTransform.anchoredPosition;
            warningImage.gameObject.SetActive(false);
        }

        private void UpdateHealthUI(float current, float max)
        {
            _slider.maxValue = max;
            
            _healthBarTween?.Kill();
            
            _healthBarTween = DOTween.To(
                () => _slider.value,
                x => _slider.value = x,
                current,
                healthBarTweenDuration
            ).SetEase(healthBarEase);
            
            float startValue = float.Parse(_healthText.text.Split('/')[0].Trim());
            DOTween.To(
                () => startValue,
                x =>
                {
                    startValue = x;
                    _healthText.text = $"{x:F0} / {max:F0}";
                },
                current,
                healthBarTweenDuration
            ).SetEase(healthBarEase);
        }

        private void WaringMotion()
        {
            Debug.Log("체력바 연출");

            warningImage.gameObject.SetActive(true);
            DOTween.Kill(healthBarTransform);
            
            _shakeTween = healthBarTransform.DOShakeAnchorPos(
                    duration: 0.5f,
                    strength: new Vector2(5f, 1f),
                    vibrato: 20,
                    randomness: 90,
                    snapping: false,
                    fadeOut: false)
                .SetLoops(-1, LoopType.Restart);
        }

        private void RecoverMotion()
        {
            Debug.Log("연출 멈추기");
         
            warningImage.gameObject.SetActive(false);
            _shakeTween?.Kill();

            healthBarTransform.DOAnchorPos(_originPosition, 0.2f)
                .SetEase(Ease.OutSine);
        }
        
        private void DeadMotion()
        {
            _canvasGroup.DOFade(0f, 0.5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => Destroy(gameObject));
        }
        
        protected void OnDestroy()
        {
            _healthBarTween?.Kill();
            _shakeTween?.Kill();
            DOTween.Kill(healthBarTransform);
            
            if (_health != null)
            {
                _health.OnHealthChanged -= UpdateHealthUI;
                _health.OnDead -= DeadMotion;
                _health.OnLowHealth -= WaringMotion;
                _health.OnRecoverHealth -= RecoverMotion;
            }
        }
    }
}