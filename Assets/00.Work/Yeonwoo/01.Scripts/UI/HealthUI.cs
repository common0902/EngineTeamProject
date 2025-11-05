using System;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class HealthUI : MonoBehaviour
    {
        private HealthSystem healthSystem;
        private TextMeshProUGUI healthText;
        
        private IHealth _health;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            if (!_slider)
                Debug.LogError("slider is null");
            
            healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
            if (!healthSystem)
                Debug.LogError("_healthSystem is null");
            
            healthText = GetComponentInChildren<TextMeshProUGUI>();
            if (!healthText)
                Debug.LogError("healthText is null");
            
            if (healthSystem is IHealth health) // HealthSystem이 IHealth를 구현했는지 확인함
            {
                // 굳이 예외 처리를 할 필요는 없지만 안전을 위해서 쓴 코드
                // 체력바 UI가 변하는걸 자동으로 구독하는 코드
                _health = health; // 의존성 주입
                _slider.maxValue = _health.MaxHealth;
                _slider.value = _health.Health;
                
                healthText.text = $"{_health.Health} / {_health.MaxHealth}";

                _health.OnHealthChanged += UpdateHealthUI;
                _health.OnDead += DeadMotion;
            }
            else
            {
                Debug.LogError("HealthUI: Assign an IHealth implementation to the healthSource.");
            }
        }

        private void UpdateHealthUI(float current, float max)
        {
            _slider.maxValue = max;
            _slider.value = current;
            healthText.text = $"{_health.Health} / {_health.MaxHealth}";
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
                healthSystem.Damage(10f);
        }
         
        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnHealthChanged -= UpdateHealthUI;
                _health.OnDead -= DeadMotion;
            }
        }
    }
}