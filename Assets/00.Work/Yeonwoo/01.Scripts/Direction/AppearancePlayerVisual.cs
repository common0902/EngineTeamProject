using System;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class AppearancePlayerVisual : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Material _material;
        private InPortalManager _portalManager;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _material = _spriteRenderer.material;
        }
        
        
    }
}