using System;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class InGameCinema : MonoBehaviour
    {
        [SerializeField] private AppearancePlayerVisual visualChange;
        [SerializeField] private AppearancePlayer player;
        [SerializeField] private float distance = 3f;
        [SerializeField] private float duration;
        private Material material;
    
        public event Action AppearanceComplete;

        private void Awake()
        {
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            material = spriteRenderer.material;
            player.transform.position = transform.position;
        }

        private void OnEnable()
        {
            visualChange.PlayerScaleChanged += PortalAnim;
        }
        
        private void PortalAnim()
        {
            Vector3 targetPos = player.transform.position + Vector3.down * distance;
        
            player.transform.DOMove(targetPos, duration)
                .SetEase(Ease.InOutQuint)
                .OnComplete(() =>
                {
                    AppearanceComplete?.Invoke();
                    DOVirtual.DelayedCall(1.5f, () =>
                    {
                        material.DOFloat(0f, "_FullGlowDissolveFade", 1f)
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
            visualChange.PlayerScaleChanged -= PortalAnim;
        }
    }
}