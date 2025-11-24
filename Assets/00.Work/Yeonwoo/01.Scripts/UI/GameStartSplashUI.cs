// GameStartSplashUI.cs
using System.Collections;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class GameStartSplashUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentStg; // 예: "currentStg(1)"
        [SerializeField] private TextMeshProUGUI stgNum;     // 예: "stgSum(1)"
        [SerializeField] private float showDuration = 4.0f;  // 화면에 보여줄 시간(초)

        private void Awake()
        {
            // 텍스트 컴포넌트가 할당 안 되어있다면 자식에서 찾아본다.
            if (currentStg == null) currentStg = GetComponentInChildren<TextMeshProUGUI>();
            // 만약 두 개를 쓰고 싶다면, 인스펙터에서 명확히 할당하세요.
        }

        private void OnEnable()
        {
            // RoomManager 인스턴스가 있으면 이벤트 구독
            if (RoomManager.Instance != null)
            {
                RoomManager.Instance.OnSetComplete += OnMapSetComplete;
            }
        }

        private void OnDisable()
        {
            if (RoomManager.Instance != null)
            {
                RoomManager.Instance.OnSetComplete -= OnMapSetComplete;
            }
        }

        // RoomManager의 맵 세팅이 완전해졌을 때 호출되는 핸들러
        private void OnMapSetComplete()
        {
            ShowCurrentStage();
        }

        // 외부에서 수동으로도 호출 가능 (예: UI가 이벤트를 놓쳤을 때)
        public void ShowCurrentStage()
        {
            if (GameManager.Instance == null)
            {
                Debug.LogWarning("[GameStartSplashUI] GameManager.Instance가 없습니다. UI를 표시할 수 없습니다.");
                return;
            }

            int world = GameManager.Instance.CurrentWorld;
            int stage = GameManager.Instance.CurrentStage;

            // 요청하신 형식으로 텍스트 설정
            if (currentStg != null)
                currentStg.text = $"현재 스테이지";

            if (stgNum != null)
                stgNum.text = $"{world} - {stage}";

            // 활성화 후 일정시간 후 비활성화
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideAfterSeconds(showDuration));
        }

        private IEnumerator HideAfterSeconds(float t)
        {
            yield return new WaitForSeconds(t);
            gameObject.SetActive(false);
        }
    }
}
