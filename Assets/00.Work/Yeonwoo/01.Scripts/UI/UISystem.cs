using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
   public class UISystem : MonoBehaviour
   {
      private SettingPanel _settingPanel;
      public bool isActive;
      
      private void Awake()
      {
         if (_settingPanel == null) 
            _settingPanel = UI.SettingPanel.Instance;
         
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
      
      protected virtual void TogglePanel()
      {
         isActive = _settingPanel.gameObject.activeSelf; 
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
