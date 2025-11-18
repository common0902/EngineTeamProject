using System;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class InGameCinema : MonoBehaviour
    {
        private Material _material;
        
        private void OnEnable()
        {
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _material = spriteRenderer.material;
            
            DOVirtual.DelayedCall(6.5f, () =>
            {
                _material.DOFloat(0f, "_FullGlowDissolveFade", 2f)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
            });
        }
    }
}