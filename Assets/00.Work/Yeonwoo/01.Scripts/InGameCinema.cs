using System;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts
{
    public class InGameCinema : MonoBehaviour
    {
        [SerializeField] private AppearancePlayerVisual _visualChange;
        [SerializeField] private AppearancePlayer _player;
        [SerializeField] private float _distance = 3f;
        [SerializeField] private float _duration;
        private Material material;
    
        public event Action AppearanceComplete;

        private void Awake()
        {
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            material = spriteRenderer.material;
            _player.transform.position = transform.position;
        }

        private void OnEnable()
        {
            _visualChange.PlayerScaleChanged += PortalAnim;
        }
        
        private void PortalAnim()
        {
            Vector3 targetPos = _player.transform.position + Vector3.down * _distance;
        
            _player.transform.DOMove(targetPos, _duration)
                .SetEase(Ease.InOutQuint)
                .OnComplete(() =>
                {
                    AppearanceComplete?.Invoke();
                    DOVirtual.DelayedCall(3f, () =>
                    {
                        material.DOFloat(0f, "_FullGlowDissolveFade", 2f)
                            .SetEase(Ease.OutSine)
                            .OnComplete(() =>
                            {
                                Destroy(gameObject);
                            });
                    });
                });
        }

        private void OnDisable()
        {
            _visualChange.PlayerScaleChanged -= PortalAnim;
        }
    }
}