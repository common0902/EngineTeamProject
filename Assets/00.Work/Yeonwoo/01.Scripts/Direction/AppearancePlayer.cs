using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class AppearancePlayer : MonoBehaviour
    {
        [SerializeField] private InGameCinema portalAnimation;
        private Material _material;
        private PlayerStateStopper _playerStateStopper;
        [SerializeField] private InPortalManager _portalManager;
        
        private void Awake()
        {
            _playerStateStopper = GetComponent<PlayerStateStopper>();
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _material = spriteRenderer.material;
        }

        private void OnEnable()
        {
            _portalManager.PlayerPortalIn += PortalInFade;
            portalAnimation.AppearanceComplete += PlayerHologram;
        }

        private void PortalInFade()
        {
            _playerStateStopper.DisableControls();
            _material.DOFloat(1f, "_HologramFade", 2.5f)
                .SetEase(Ease.Flash)
                .OnComplete(() =>
                {
                    _portalManager.PlayerPortalIn -= PortalInFade;
                    _portalManager.NextScene();
                });
        }

        private void OnDisable()
        {
            portalAnimation.AppearanceComplete -= PlayerHologram;
        }

        private void PlayerHologram()
        {
            _playerStateStopper.EnableControls();
            _material.DOFloat(0f, "_HologramFade", 1f)
                .SetEase(Ease.Flash);
        }
    }
}