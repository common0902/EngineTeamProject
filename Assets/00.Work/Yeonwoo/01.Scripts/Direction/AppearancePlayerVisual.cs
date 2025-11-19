using System;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class AppearancePlayerVisual : MonoBehaviour
    {
        private readonly Vector3 _playerScale = new Vector3(1f, 1f, 0);
        private PlayerStateStopper _playerStateStopper;
        
        public event Action PlayerScaleChanged;
        
        private void Awake()
        {
            _playerStateStopper = GetComponentInParent<PlayerStateStopper>();
            transform.localScale = new Vector3(0.01f, 0.01f, 0);
        }

        private void Start()
        {
            AppearancePlayer();
        }

        private void AppearancePlayer()
        {
            _playerStateStopper.DisableControls();
            transform.DOScale(_playerScale, 0.8f)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(1f, () => PlayerScaleChanged?.Invoke());
                });
        }
    }
}