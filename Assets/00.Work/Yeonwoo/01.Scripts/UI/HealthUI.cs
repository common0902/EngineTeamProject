using System;
using _00.Work.SYH._02Script.ETG;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private RectTransform healthBarTransform;

        private Vector3 _originPosition;
        private Tween _shakeTween;
        
        private HealthSystem _healthSystem;
        private TextMeshProUGUI _healthText;
        
        private IHealth _health;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            if (!_slider)
                Debug.LogError("slider is null");
            
            _healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
            if (!_healthSystem)
                Debug.LogError("healthSystem is null");
            
            _healthText = GetComponentInChildren<TextMeshProUGUI>();
            if (!_healthText)
                Debug.LogError("healthText is null");
            
            if (_healthSystem is IHealth health) // HealthSystem이 IHealth를 구현했는지 확인함
            {
                // 굳이 예외 처리를 할 필요는 없지만 안전을 위해서 쓴 코드
                // 체력바 UI가 변하는걸 자동으로 구독하는 코드
                _health = health; // 의존성 주입
                _slider.maxValue = _health.MaxHealth;
                _slider.value = _health.Health;
                
                _healthText.text = $"{_health.Health} / {_health.MaxHealth}";

                _health.OnHealthChanged += UpdateHealthUI;
                _health.OnDead += DeadMotion;
                _health.OnLowHealth += WaringMotion;
                _health.OnRecoverHealth += RecoverMotion;
            }
            else
            {
                Debug.LogError("HealthUI: Assign an IHealth implementation to the healthSource.");
            }
        }

        private void Start()
        {
            _originPosition = healthBarTransform.anchoredPosition;
        }

        private void UpdateHealthUI(float current, float max)
        {
            _slider.maxValue = max;
            _slider.value = current;
            _healthText.text = $"{_health.Health} / {_health.MaxHealth}";
        }

        private void WaringMotion()
        {
            Debug.Log("체력바 연출");

            DOTween.Kill(healthBarTransform);
            
            _shakeTween = healthBarTransform.DOShakeAnchorPos(
                    duration: 0.5f,             // 흔들리는 시간
                    strength: new Vector2(5f, 1f), // 좌우 흔들림 강도
                    vibrato: 20,                // 진동 횟수
                    randomness: 90,             // 랜덤성
                    snapping: false,
                    fadeOut: false)
                .SetLoops(-1, LoopType.Restart); // 무한 반복
            // 체력이 적을 때 체력바 연출을 여기다가 넣을 수 있음.
        }

        private void RecoverMotion()
        {
            Debug.Log("연출 멈추기");
            
            _shakeTween?.Kill();

            // 원래 위치로 복귀
            healthBarTransform.DOAnchorPos(_originPosition, 0.2f)
                .SetEase(Ease.OutSine);
            // 체력이 적은 상태에서 정상으로 돌아왔을 때 연출 돌리는 코드
        }
        
        private void DeadMotion()
        {
            // 체력이 0이 됐을 때 체력바 연출을 여기다가 넣을 수 있음.
            // gameObject.SetActive(false);
        }
        
        private void Update()
        {
            // 테스트용. 잘 되는 거 확인함
            if (Input.GetKeyDown(KeyCode.E))
                _healthSystem.Damage(10f);
            if (Input.GetKeyDown(KeyCode.Space))
                _healthSystem.Heal(10f);
        }
         
        private void OnDestroy()
        {
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