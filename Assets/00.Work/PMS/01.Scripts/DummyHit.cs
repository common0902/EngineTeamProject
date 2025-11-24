using _00.Work.SYH._02Script.ETG;
using System;
using System.Collections;
using UnityEngine;

public class DummyHit : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Animator _animator;
    private bool _isHit = false; // 피격 중 여부

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _healthSystem.OnHealthChanged += Damage;
    }

    private void Damage(float arg1, float arg2)
    {
        // 이미 피격 중이면 무시
        if (_isHit) return;

        StartCoroutine(HitCoroutine());
    }

    private IEnumerator HitCoroutine()
    {
        _isHit = true;
        _animator.SetBool("Hit", true);

        // 애니메이션 길이만큼 대기 (또는 고정 시간)
        yield return new WaitForSeconds(0.3f); // 애니메이션 길이에 맞게 조정

        _animator.SetBool("Hit", false);
        _isHit = false;
    }

    // AnimationEnd는 이제 필요 없음 (또는 백업용으로 남겨둠)
    public void AnimationEnd()
    {
        _animator.SetBool("Hit", false);
        _isHit = false;
    }
}