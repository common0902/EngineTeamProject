using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransitionManager : MonoBehaviour
{
    public static RoomTransitionManager Instance { get; private set; }

    [SerializeField] private RectTransform blackPanel;   // Canvas 밑 검은 Image 패널
    [SerializeField] private float totalDuration = 0.4f; // 전체 전환 시간 (초)

    public bool IsTransitioning => _isTransitioning;

    bool _isTransitioning;
    Image _image;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (blackPanel != null)
        {
            _image = blackPanel.GetComponent<Image>();
            blackPanel.gameObject.SetActive(false);

            if (_image != null)
            {
                var c = _image.color;
                c.a = 1f;          // 항상 완전 검게
                _image.color = c;
            }
        }
    }

    public void PlayTransition(Vector2Int moveDir, Action midAction, Action onComplete = null)
    {
        if (_isTransitioning || blackPanel == null)
            return;

        _isTransitioning = true;

        float oldTimeScale = Time.timeScale;
        Time.timeScale = 0f; // 전환 동안 게임 멈춤

        blackPanel.gameObject.SetActive(true);

        var canvasRect = blackPanel.parent as RectTransform;
        float w = canvasRect.rect.width;
        float h = canvasRect.rect.height;

        Vector2 center = Vector2.zero;
        Vector2 start = center;
        Vector2 end = center;

        // moveDir = 플레이어 실제 이동 방향 (왼쪽/오른쪽/위/아래)
        if (moveDir == Vector2Int.left)
        {
            start = new Vector2(-w, 0);
            end = new Vector2(w, 0);
        }
        else if (moveDir == Vector2Int.right)
        {
            start = new Vector2(w, 0);
            end = new Vector2(-w, 0);
        }
        else if (moveDir == Vector2Int.up)
        {
            start = new Vector2(0, h);
            end = new Vector2(0, -h);
        }
        else if (moveDir == Vector2Int.down)
        {
            start = new Vector2(0, -h);
            end = new Vector2(0, h);
        }

        blackPanel.anchoredPosition = start;

        float half = totalDuration * 0.5f;

        var seq = DOTween.Sequence();
        seq.SetUpdate(true); // timeScale 0이어도 진행 (unscaled time)

        // 1) 화면 안쪽으로 슬라이드해 와서 완전히 덮기 (일정 속도)
        seq.Append(blackPanel.DOAnchorPos(center, half).SetEase(Ease.Linear));

        // 2) 덮인 순간에 방/플레이어 위치 변경
        seq.AppendCallback(() => midAction?.Invoke());

        // 3) 반대쪽으로 빠져나가면서 화면 열기 (일정 속도)
        seq.Append(blackPanel.DOAnchorPos(end, half).SetEase(Ease.Linear));

        seq.OnComplete(() =>
        {
            blackPanel.gameObject.SetActive(false);
            blackPanel.anchoredPosition = center;

            Time.timeScale = oldTimeScale;
            _isTransitioning = false;
            onComplete?.Invoke();
        });
    }
}
