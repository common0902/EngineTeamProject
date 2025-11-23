using System;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class PortalInPlayerVisual : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Material _material;
        private InPortalManager _portalManager;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _material = _spriteRenderer.material;
        }
        
        private void InPortalPlayerVisual(Action onComplete)
        {
            _material.DOFloat(1f, "_HologramFade", 2.0f);
        }
        
        private void Update()
        {
            GameObject endPortal = GameObject.Find("EndPortal");
            if (endPortal != null)
            {
                _portalManager = endPortal.GetComponent<InPortalManager>();
                _portalManager.PlayerPortalIn += InPortalPlayerVisual;
            }
            if (endPortal == null)
                Debug.Log("12313");
        }
        
        private void OnDisable()
        {
            _portalManager.PlayerPortalIn += InPortalPlayerVisual;
        }
    }
}