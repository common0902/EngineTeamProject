using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public static class SettingsBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            ApplyAllSettings();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ApplyAllSettings();
        }

        private static void ApplyAllSettings()
        {
            VideoSettingPanel.ApplySavedSettings();
            
            var soundPanel = Object.FindAnyObjectByType<SoundSettingPanel>(FindObjectsInactive.Include);
            if (soundPanel != null)
            {
                soundPanel.ApplySavedVolumeSettings();
            }
        }
    }
}
