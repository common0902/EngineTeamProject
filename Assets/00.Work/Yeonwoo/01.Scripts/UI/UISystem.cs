using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using _00.Work.Yeonwoo._01.Scripts.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class UISystem : MonoBehaviour
    {
        protected SettingPanel _settingPanel;
        protected List<IMenuPanel> _subPanels = new();

        protected bool IsActive;

        private void Awake()
        {
            _settingPanel = UI.SettingPanel.Instance;
            _settingPanel.gameObject.SetActive(false);
            
            TryRegisterPanel(SoundSettingPanel.Instance);
            TryRegisterPanel(VideoSettingPanel.Instance);
        }

        private void TryRegisterPanel(IMenuPanel panel)
        {
            if (panel != null && !_subPanels.Contains(panel))
                _subPanels.Add(panel);
        }

        public void GameStart()
        {
            SceneManager.LoadScene("PrologScene");
            Time.timeScale = 1f;
        }

        public void Exit()
        {
            Application.Quit();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleEsc();
            }
        }

        protected virtual void HandleEsc()
        {
            foreach (var panel in _subPanels)
            {
                if (panel.IsOpen)
                {
                    panel.Close();
                    return;
                }
            }
            
            if (_settingPanel.gameObject.activeSelf)
            {
                _settingPanel.SettingPanelClose();
                return;
            }
            
            _settingPanel.SettingPanelOpen();
        }

        public void SettingPanel()
        {
            _settingPanel.SettingPanelOpen();
        }
    }
    
    public interface IMenuPanel
    {
        bool IsOpen { get; }
        void Open();
        void Close();
    }
}
