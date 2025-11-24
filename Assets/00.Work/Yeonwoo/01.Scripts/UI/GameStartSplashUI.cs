// GameStartSplashUI.cs (수정본)
using System.Collections;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class GameStartSplashUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentStg; // 예: "현재 스테이지"
        [SerializeField] private TextMeshProUGUI stgNum;     // 예: "1 - 1"
        [SerializeField] private float showDuration = 4.0f;  // 화면에 보여줄 시간(초)

        private void Awake()
        {
            // currentStg, stgNum이 인스펙터에 없으면 자식에서 찾아서 채운다.
            var allTmps = GetComponentsInChildren<TextMeshProUGUI>(true);

            if (currentStg == null)
            {
                // 우선 이름 기준으로 찾기: "currentStg" 이름을 쓴다면 그걸 우선
                foreach (var t in allTmps)
                {
                    if (t.name.ToLower().Contains("current") || t.name.ToLower().Contains("stage"))
                    {
                        currentStg = t;
                        break;
                    }
                }
                // 못 찾으면 첫 번째를 사용
                if (currentStg == null && allTmps.Length > 0) currentStg = allTmps[0];
            }

            if (stgNum == null)
            {
                // currentStg과 다른 TextMeshProUGUI를 stgNum으로 사용
                foreach (var t in allTmps)
                {
                    if (t == currentStg) continue;
                    stgNum = t;
                    break;
                }
            }
        }

        private void OnEnable()
        {
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
            StopAllCoroutines();
        }

        private void OnMapSetComplete()
        {
            ShowCurrentStage();
        }

        // 외부에서 수동으로도 호출 가능 (예: GameManager에서 호출)
        public void ShowCurrentStage()
        {
            if (GameManager.Instance == null)
            {
                Debug.LogWarning("[GameStartSplashUI] GameManager.Instance가 없습니다. UI를 표시할 수 없습니다.");
                return;
            }

            int world = GameManager.Instance.CurrentWorld;
            int stage = GameManager.Instance.CurrentStage;

            if (currentStg != null)
                currentStg.text = $"현재 스테이지";

            if (stgNum != null)
                stgNum.text = $"{world} - {stage}";

            // 재트리거 보장: 이미 활성화 상태라도 비활성화 후 활성화해서 OnEnable/애니메이션을 확실히 다시 실행
            StopAllCoroutines();


            StartCoroutine(HideAfterSeconds(showDuration));
        }

        private IEnumerator HideAfterSeconds(float t)
        {
            yield return new WaitForSeconds(t);
            // UI가 더 이상 보여질 필요가 없으면 비활성화
        }
    }
}
