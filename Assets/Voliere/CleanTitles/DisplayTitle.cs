// Voliere.CleanTitles.DisplayTitle.cs
using TMPro;
using UnityEngine;

namespace Voliere.CleanTitles {
    public class DisplayTitle : MonoBehaviour {
        public TextMeshProUGUI[] title;
        public TextMeshProUGUI[] subtitle;

        [Tooltip("씬 재생 시 자동으로 애니메이션(Enable) 되지 않게 하려면 false로 설정")]
        [SerializeField] private bool playOnEnable = false;

        private void OnEnable() {
            // playOnEnable이 false일 때는 OnEnable에서 애니메이션을 직접 트리거하지 않음
            // (애니메이션이 OnEnable로 자동 재생되는 설정이면 이 플래그로 차단)
            if (playOnEnable) {
                // 기존 동작(원한다면 구현)
            }
        }

        public void Show(string titleStr) {
            Show(titleStr, "");
        }

        public void Show(string titleStr, string subtitleStr) {
            // 빈 문자열이면 아무 것도 재생하지 않음
            if (string.IsNullOrEmpty(titleStr) && string.IsNullOrEmpty(subtitleStr)) return;

            if (title != null) {
                foreach (TextMeshProUGUI aTitle in title) {
                    aTitle.text = titleStr ?? "";
                }
            }
            if (subtitle != null) {
                foreach (TextMeshProUGUI aSubtitle in subtitle) {
                    aSubtitle.text = subtitleStr ?? "";
                }
            }

            // 확실히 활성화해서 애니메이션 트리거
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}