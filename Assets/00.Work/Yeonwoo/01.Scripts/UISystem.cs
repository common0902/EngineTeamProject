using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts
{
   public class UISystem : MonoBehaviour
   {
      private SettingPanel _settingPanel;

      private void Awake()
      {
         if (_settingPanel == null) 
            _settingPanel = global::SettingPanel.Instance;
         
         if (_settingPanel == null) 
            Debug.Log("3or1fj1f");
         
         if (_settingPanel != null) 
            _settingPanel.gameObject.SetActive(false);
      }

      public void GameStart()
      {
         SceneManager.LoadScene(1);
      }

      public void Exit()
      {
         Application.Quit();
      }
      
      private void TogglePanel()
      {
         bool isActive = _settingPanel.gameObject.activeSelf; 
         _settingPanel.gameObject.SetActive(!isActive);
      }
    
      private void Update()
      {
         if (Input.GetKeyDown(KeyCode.Escape))
         {
            TogglePanel();
         }
      }

      public void SettingPanel()
      {
         _settingPanel.SettingPanelOpen();
      }
   }
}
