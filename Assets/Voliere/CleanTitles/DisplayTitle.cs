using TMPro;
using UnityEngine;

namespace Voliere.CleanTitles {
    public class DisplayTitle : MonoBehaviour {
        /// <summary>
        /// The TMPro title component(s).
        /// </summary>
        public TextMeshProUGUI[] title;
        /// <summary>
        /// The TMPro subtitle component(s). Optional, and not supported by all animations!
        /// </summary>
        public TextMeshProUGUI[] subtitle;

        /// <summary>
        /// Animate this title in with the provided text.
        /// </summary>
        /// <param name="title">String to use as the main title.</param>
        public void Show(string title) {
            Show(title, "");
        }

        /// <summary>
        /// Animate this title in with the provided title and subtitle.
        /// </summary>
        /// <param name="title">String to use as the main title.</param>
        /// <param name="subtitle">String to use as the subtitle. Optional, and not supported in all animations!</param>
        public void Show(string titleStr, string subtitleStr) {
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
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}