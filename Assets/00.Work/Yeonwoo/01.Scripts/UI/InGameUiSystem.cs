using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class InGameUiSystem : UISystem
    {
        protected override void HandleEsc()
        {
            foreach (var panel in _subPanels)
            {
                if (panel.IsOpen)
                {
                    panel.Close();
                    Time.timeScale = 0f;
                    return;
                }
            }
            
            if (_settingPanel.gameObject.activeSelf)
            {
                _settingPanel.SettingPanelClose();
                Time.timeScale = 1f;
                return;
            }
            
            _settingPanel.SettingPanelOpen();
            Time.timeScale = 0f;
        }
    }
}