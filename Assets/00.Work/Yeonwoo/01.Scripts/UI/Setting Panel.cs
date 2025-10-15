using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SettingPanel : MonoSingleton<SettingPanel>
    {
        [SerializeField] private SoundSettingPanel  _soundSettingPanel;
        [SerializeField] private VideoSettingPanel _videoSettingPanel;

        public void SoundPanelOpen()
        {
            _soundSettingPanel.gameObject.SetActive(true);
        }

        public void VideoPanelOpen()
        {
            _videoSettingPanel.gameObject.SetActive(true);
        }
    
        public void GoTitle()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }

        public void Exit()
        {
            Application.Quit();
        }
    
        public void SettingPanelOpen()
        {
            gameObject.SetActive(true);
        }

        public void SettingPanelClose()
        {
            gameObject.SetActive(false);
        }
    }
}
