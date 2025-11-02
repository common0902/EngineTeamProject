using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SettingPanel : MonoSingletonUI<SettingPanel>
    {
        private SoundSettingPanel  _soundSettingPanel;
        private VideoSettingPanel _videoSettingPanel;
        private SelectTitlePanel _selectTitlePanel;
        private SelectExitPanel _selectExitPanel;

        protected override void Awake()
        {
            base.Awake();
            _soundSettingPanel = SoundSettingPanel.Instance;
            _videoSettingPanel = VideoSettingPanel.Instance;
            _selectTitlePanel = SelectTitlePanel.Instance;
            _selectExitPanel = SelectExitPanel.Instance;
        }

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
