using UnityEngine;

namespace Voliere.CleanTitles.Demo {
    public class DemoController : MonoBehaviour {
        public string title;
        public string subtitle;
        public DisplayTitle[] titles;

        public void Activate(DisplayTitle toActivate) {
            foreach (DisplayTitle title in titles) {
                title.gameObject.SetActive(false);
            }

            toActivate.Show(title, subtitle);
        }
    }
}