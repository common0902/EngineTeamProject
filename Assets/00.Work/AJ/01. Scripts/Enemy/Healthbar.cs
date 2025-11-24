using _00.Work.SYH._02Script.ETG;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject hpbar;
    private Enemy _enemy;
    private HealthSystem _enemyHealth;
    private float _targetFill;
    public void Init(HealthSystem enemyhealth)
    {
        _enemyHealth = enemyhealth;
        _enemyHealth.OnHealthChanged += OnDamage;
        _enemyHealth.OnDead += OnDead;
        _enemy = enemyhealth.GetComponent<Enemy>();
        if (_enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            _enemy.GetComponentInChildren<EnemyAnimator>().OnAssassinVanish += () => ShowOrHideHealthBar(false);
            _enemy.GetComponentInChildren<EnemyAnimator>().OnAssassinAppearBehind += () => ShowOrHideHealthBar(true);
        }

        _targetFill = 1f;
    }

    public void ShowOrHideHealthBar(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (_enemyHealth == null) return;
        float scalex = Mathf.Lerp(hpbar.transform.localScale.x, _targetFill, Time.deltaTime * 5);
        hpbar.transform.localScale = new Vector3(scalex, hpbar.transform.localScale.y, hpbar.transform.localScale.z);
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
        if (_enemy.enemySO != null && _enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            _enemy.GetComponentInChildren<EnemyAnimator>().OnAssassinVanish -= () => ShowOrHideHealthBar(false);
            _enemy.GetComponentInChildren<EnemyAnimator>().OnAssassinAppearBehind -= () => ShowOrHideHealthBar(true);
        }
    }
}
