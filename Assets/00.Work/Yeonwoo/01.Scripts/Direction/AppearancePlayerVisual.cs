using System;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts
{
    public class AppearancePlayerVisual : MonoBehaviour
    {
        [SerializeField] private Vector3 _playerScale = new Vector3(0.65f, 0.65f, 0);
        
        public event Action PlayerScaleChanged;
        private void Awake()
        {
            transform.localScale = new Vector3(0.01f, 0.01f, 0);
        }

        private void Start()
        {
            AppearancePlayer();
        }

        private void AppearancePlayer()
        {
            transform.DOScale(_playerScale, 2.3f)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(1f, () => PlayerScaleChanged?.Invoke());
                });
        }
    }
}