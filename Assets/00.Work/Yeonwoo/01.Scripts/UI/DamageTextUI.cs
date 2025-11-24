using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class DamageTextUI : MonoBehaviour, IPoolable
    {
        public string ItemName => itemName;
        [SerializeField] private string itemName = "DamageText";
        public GameObject GameObject => gameObject;
        
        [Header("Settings")] 
        [SerializeField] private float duration = 1.3f;
        [SerializeField] private Vector3 riseOffset = new Vector3(0f, 2f, 0f);
        [SerializeField] private AnimationCurve alphaCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
        [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        private TextMeshProUGUI _text;
        
        private CanvasGroup _canvasGroup;
        private Camera _mainCamera;
        private Vector3 _worldPos;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
            _mainCamera = Camera.main;
        }

        public void SetDamage(float amount, Vector3 worldPos, Color color)
        {
            _worldPos = worldPos;
            _text.text = amount.ToString("F2");
            _text.color = color;

            StopAllCoroutines();
            StartCoroutine(ShowDtxCoroutine());
        }

        private IEnumerator ShowDtxCoroutine()
        {
            float t = 0f;
            Vector3 start = _worldPos;
            Vector3 end = _worldPos + riseOffset;
            RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            RectTransform rectTransform = GetComponent<RectTransform>();
            
            while (t < duration)
            {
                t += Time.deltaTime;
                float normalized = t / duration;

                Vector3 world = Vector3.Lerp(start, end, moveCurve.Evaluate(normalized));
                Vector3 screen = _mainCamera.WorldToScreenPoint(world);
                
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvasRect, screen, _mainCamera, out Vector2 localPoint))
                {
                    rectTransform.localPosition = localPoint;
                }
                
                _canvasGroup.alpha = alphaCurve.Evaluate(normalized);

                yield return null;
            }

            PoolManager2.Instance.Push(this);    
        }

        public void ResetItem()
        {
            _canvasGroup.alpha = 1;
            transform.localScale = Vector3.one;
            gameObject.SetActive(true);
        }
    }
}