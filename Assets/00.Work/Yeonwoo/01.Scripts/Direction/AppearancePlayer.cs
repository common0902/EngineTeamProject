using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts
{
    public class AppearancePlayer : MonoBehaviour
    {
        [SerializeField] private InGameCinema _portalAnim;
        private Material _material;

        private void Awake()
        {
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _material = spriteRenderer.material;
        }

        private void OnEnable()
        {
            _portalAnim.AppearanceComplete += PlayerHologram;
        }

        private void OnDisable()
        {
            _portalAnim.AppearanceComplete -= PlayerHologram;
        }

        private void PlayerHologram()
        {
            _material.DOFloat(0f, "_HologramFade", 1f)
                .SetEase(Ease.Flash);
        }
    }
}