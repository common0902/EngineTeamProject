using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SelectTitlePanel : MonoSingletonUI<SelectTitlePanel>, ISettingPanel
    {
        public bool IsOpen => gameObject.activeSelf;

        public void GoTitle()
        {
            SceneManager.LoadScene("Title");
            Time.timeScale = 1;
        }
        
        public void Open() => gameObject.SetActive(true);

        public void Close() => gameObject.SetActive(false);
    }
}
