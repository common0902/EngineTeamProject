using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using _00.Work.Yeonwoo._01.Scripts.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SettingSystem : MonoBehaviour
    {
        private SettingPanel _settingPanel;
        private readonly List<ISettingPanel> _subPanels = new();
        
        private void Awake()
        {
            try
            {
                Debug.Log("Try to setting panel's construction, SettingPanelClose()");
                _settingPanel.SettingPanelClose();
            }
            catch (NullReferenceException)
            {
                Debug.Log("try-catch.");
                Debug.Log("Setting panel Instance doesn't exist, Spawn setting panel Instance to.");
                _settingPanel = UI.SettingPanel.Instance;
            }
            
            VideoSettingPanel.ApplySavedSettings();
            
            if (_settingPanel != null) 
                _settingPanel.SettingPanelClose();
            
            if (_settingPanel == null)
                Debug.LogError("Setting panel Instance doesn't exist, Check to try-catch construction");
            
            TryRegisterPanel(SoundSettingPanel.Instance);
            TryRegisterPanel(VideoSettingPanel.Instance);
            TryRegisterPanel(SelectTitlePanel.Instance);
            TryRegisterPanel(SelectExitPanel.Instance);
        }
        
        private void TryRegisterPanel(ISettingPanel panel)
        {
            if (panel != null && !_subPanels.Contains(panel))
                _subPanels.Add(panel);
        }

        public void GameStart()
        {
            SceneManager.LoadScene("Develop");
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

        protected void HandleEsc() // 문제 발생 지점 코드였지만... 고쳤다!!
        {
            foreach (var panel in _subPanels)
            {
                if (panel.IsOpen)
                {
                    panel.Close();
                    _settingPanel.gameObject.SetActive(true);
                    PanelActiveHelper();
                    return;
                }
            }
            
            if (_settingPanel.gameObject.activeSelf)
            {
                _settingPanel.SettingPanelClose();
                PanelActiveHelper();
                return;
            }
            
            _settingPanel.SettingPanelOpen();
            PanelActiveHelper();
        }

        private void PanelActiveHelper() // 얘를 추가해도 달라진건 없었지만... 달라졌다!!
        {
            bool isSettingPanelOpen = _settingPanel.gameObject.activeSelf;
            bool isAnySubPanelOpen = false;
    
            foreach (var panel in _subPanels)
            {
                if (panel.IsOpen)
                {
                    isAnySubPanelOpen = true;
                    break;
                }
            }

            bool anyPanelOpen = isSettingPanelOpen || isAnySubPanelOpen;
            Time.timeScale = anyPanelOpen ? 0f : 1f;
        }
        
        public void SettingPanel()
        {
            _settingPanel.SettingPanelOpen();
        }
    }
}
