using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SettingPanel : Data.MonoSingletonUI<SettingPanel>
    {
        [SerializeField] private SoundSettingPanel  _soundSettingPanel;
        [SerializeField] private VideoSettingPanel _videoSettingPanel;
        [SerializeField] private SelectTitlePanel _selectTitlePanel;
        [SerializeField] private SelectExitPanel _selectExitPanel;

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
            _selectTitlePanel.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _selectExitPanel.gameObject.SetActive(true);
        }
    
        public void SettingPanelOpen()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        public void SettingPanelClose()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
