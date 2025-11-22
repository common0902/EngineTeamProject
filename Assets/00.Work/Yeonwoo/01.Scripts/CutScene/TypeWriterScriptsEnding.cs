using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.CutScene
{
    public class TypeWriterScriptsEnding : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] RectTransform content;
        [SerializeField] RectTransform viewport;
    
        [Header("Scroll Settings")]
        [SerializeField] float pixelsPerSecond = 30f;
        [SerializeField] bool loop = false;
        [SerializeField] float startDelay = 0.5f;

        private Coroutine _runner;
    
        private void Start()
        {
            StartScroll();
        }
    
        private void StartScroll()
        {
            if (content == null || viewport == null)
            {
                Debug.LogError("CreditScroller: content 또는 viewport 할당 필요");
                return;
            }
            
            Canvas.ForceUpdateCanvases();
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(content);
    
            if (_runner != null) StopCoroutine(_runner);
            _runner = StartCoroutine(ScrollRoutine());
        }
    
        private IEnumerator ScrollRoutine()
        {
            Vector2 startPos = content.anchoredPosition;
            
            float distance = content.rect.height + viewport.rect.height;
            Vector2 endPos = startPos + Vector2.up * distance;
            
            if (pixelsPerSecond <= 0f)
            {
                Debug.LogWarning("pixelsPerSecond가 0 이하입니다. 스크롤이 진행되지 않습니다.");
                yield break;
            }
            
            if (startDelay > 0f) yield return new WaitForSeconds(startDelay);
    
            float duration = distance / pixelsPerSecond;
            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Clamp01(t / duration);
                content.anchoredPosition = Vector2.Lerp(startPos, endPos, alpha);
                yield return null;
            }
    
            content.anchoredPosition = endPos;
    
            if (loop)
            {
                yield return new WaitForSeconds(0.5f);
                content.anchoredPosition = startPos;
                _runner = StartCoroutine(ScrollRoutine());
            }
            else
            {
                _runner = null;
            }
        }
    }
}