using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image hpbar; 
    private HealthSystem _enemyHealth;
    private float _targetFill;
    public void Init(HealthSystem enemyhealth)
    {
        _enemyHealth = enemyhealth;
        _enemyHealth.OnHealthChanged += OnDamage;
        _enemyHealth.OnDead += OnDead;
        
        _targetFill = 1f;
        hpbar.fillAmount = _targetFill;
    }


    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (_enemyHealth == null) return;
        hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount, _targetFill, Time.deltaTime);
    }

    private void OnDamage(float health, float maxHealth)
    {
        _targetFill = Mathf.Clamp01(health / maxHealth);
    }
    
    private void OnDead()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_enemyHealth != null)
        {
            _enemyHealth.OnHealthChanged -= OnDamage;
            _enemyHealth.OnDead -= OnDead;
        }
    }
}
