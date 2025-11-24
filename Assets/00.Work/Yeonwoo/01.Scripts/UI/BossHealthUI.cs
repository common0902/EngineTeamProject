using System;
using _00.Work.PMS._01.Scripts;
using _00.Work.SYH._02Script.ETG;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class BossHealthUI : MonoBehaviour
    {
        [SerializeField] private float healthTweenDuration = 0.3f;
        [SerializeField] private Ease healthTweenEase = Ease.OutCubic;

        private Slider _slider;
        private IHealth _healthSource;
        private Tween _fillTween;
        private HealthSystem _healthSystem;
        private MiddleBossRoom _bossRoom;
        
        private void Awake()
        {
            _bossRoom = GetComponentInParent<MiddleBossRoom>();

            _slider = GetComponent<Slider>();
            if (_slider == null)
            {
                Debug.LogError("BossHealthUI: Slider 컴포넌트를 찾을 수 없습니다.");
            }

            _healthSystem = _bossRoom.HealthSystem;
            if (_healthSystem is IHealth ih)
            {
                _healthSource = ih;
            }
            else
            {
                Debug.LogError("BossHealthUI: 부모에 IHealth(HealthSystem)를 찾을 수 없습니다.");
            }
        }

        private void Start()
        {
            if (_healthSource == null || _slider == null) return;

            _slider.maxValue = _healthSource.MaxHealth;
            _slider.value = _healthSource.Health;

            _healthSource.OnHealthChanged += OnHealthChanged;
            _healthSource.OnDead += BarDestroy;
        }

        private void BarDestroy()
        {
            Destroy(gameObject);
        }
        
        private void OnHealthChanged(float current, float max)
        {
            if (_slider == null) return;

            _slider.maxValue = max;

            _fillTween?.Kill();

            _fillTween = DOTween.To(() => _slider.value, x => _slider.value = x, current, healthTweenDuration)
                .SetEase(healthTweenEase);
        }

        private void OnDestroy()
        {
            _fillTween?.Kill();
            if (_healthSource != null)
                _healthSource.OnHealthChanged -= OnHealthChanged;
            if (_healthSource != null) _healthSource.OnDead -= BarDestroy;
        }
    }
}