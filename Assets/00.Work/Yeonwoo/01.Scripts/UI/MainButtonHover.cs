using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class MainButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform _targetRect;  
        [SerializeField] private float hoverWidth = 300f;
        [SerializeField] private float duration = 0.2f;
        
        private float _originalWidth;

        private void Awake()
        {
            if (_targetRect == null)
                _targetRect = GetComponent<RectTransform>();

            _originalWidth = _targetRect.sizeDelta.x;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _targetRect.DOKill();  
            _targetRect.DOSizeDelta(
                new Vector2(hoverWidth, _targetRect.sizeDelta.y),
                duration
            ).SetEase(Ease.OutQuad);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _targetRect.DOKill();
            _targetRect.DOSizeDelta(
                new Vector2(_originalWidth, _targetRect.sizeDelta.y),
                duration
            ).SetEase(Ease.OutQuad);
        }

        private void OnDisable()
        {
            _targetRect.DOKill();
        }
    }
}