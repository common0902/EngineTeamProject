using System;
using _00.Work.SYH._02Script.ETG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _00.Work.Yeonwoo._01.Scripts.Direction;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;

namespace _00.Work.Yeonwoo._01.Scripts.CutScene
{
    public class PlayerDeathCinema : MonoBehaviour
    {
        private HealthSystem _health;
        private PlayerStateStopper _stopState;

        [SerializeField] private Image deathOverlay;
        [SerializeField] private Fire deathEffectBehaviour;
        private IDeathCinema _deathEffect;

        public Action OnCinemaComplete; // 사망 연출 끝나고 UI 띄우기용

        private void Awake()
        {
            _stopState = GetComponent<PlayerStateStopper>();
            _health = GetComponent<HealthSystem>();

            if (deathOverlay == null)
                Debug.LogWarning("Death overlay(Image) is not assigned in PlayerDeathCinema.");

            if (deathEffectBehaviour is IDeathCinema effect)
                _deathEffect = effect;
            else
                Debug.LogWarning("Death effect is not assigned or does not implement IDeathEffect.");
        }

        private void Start()
        {
            if (_health != null)
                _health.OnDead += DeadCinema;
            else
                Debug.LogWarning("HealthSystem is null on PlayerDeathCinema.");
        }

        private void DeadCinema()
        {
            _health.OnDead -= DeadCinema;
            _stopState?.DisableControls();

            Sequence seq = DOTween.Sequence().SetUpdate(true);
    
            if (deathOverlay != null)
            {
                Color c = deathOverlay.color;
                c.a = 0f;
                deathOverlay.color = c;
                seq.Append(deathOverlay.DOFade(1f, 0.9f));
            }
            else
            {
                seq.AppendInterval(1.2f);
            }
    
            seq.AppendCallback(() =>
            {
            if (deathEffectBehaviour != null)
            {
                var overlayCanvas = deathOverlay != null ? deathOverlay.GetComponentInParent<Canvas>() : null;
                var fireCanvas = deathEffectBehaviour.GetComponentInParent<Canvas>();

            if (overlayCanvas != null && fireCanvas != null)
            {
                if (overlayCanvas == fireCanvas)
                {
                    var overlayIdx = deathOverlay.transform.GetSiblingIndex();
                    deathEffectBehaviour.transform.SetSiblingIndex(Mathf.Min(overlayIdx + 1, deathEffectBehaviour.transform.parent.childCount - 1));
                }
                else
                {
                    try
                    {
                        fireCanvas.sortingOrder = overlayCanvas.sortingOrder + 1;
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"Canvas sortingOrder adjust failed: {e.Message}");
                    }
                }
            }
            else
            {
                try
                {
                    int overlayIdx = deathOverlay != null ? deathOverlay.transform.GetSiblingIndex() : 0;
                    deathEffectBehaviour.transform.SetSiblingIndex(overlayIdx + 1);
                }
                catch
                {
                    // 무시
                }
            }
            
            deathEffectBehaviour.gameObject.SetActive(true);
            }
            });
    
    if (_deathEffect != null)
    {
        seq.Append(_deathEffect.PlayEntrance(1.5f, true));
        seq.Append(_deathEffect.PlayDead(3f, true));
    }
    else
    {
        seq.AppendInterval(2.8f);
    }
    
    seq.AppendCallback(() =>
    {
        OnCinemaComplete?.Invoke();
        Debug.Log("UI 뜨기");
    });

    seq.Play();
}

        
        private void OnDestroy()
        {
            if (_health != null)
                _health.OnDead -= DeadCinema;
        }
    }
}
