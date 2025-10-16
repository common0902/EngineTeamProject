using System;
using System.Collections;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class LoadSetting : MonoBehaviour
    { 
        private void Awake()
        {
            StartCoroutine(ApplyVideoSettingsAfterFrame());
        }

        private IEnumerator ApplyVideoSettingsAfterFrame()
        {
            yield return null;
            try
            {
                VideoSettingPanel.ApplySavedSettings();
            }
            catch (Exception e)
            {
                VideoSettingPanel videoSettingPanel =  FindAnyObjectByType<VideoSettingPanel>(FindObjectsInactive.Include);
                if (videoSettingPanel != null)
                {
                    VideoSettingPanel.ApplySavedSettings();
                }
            }
            yield return new WaitForEndOfFrame();
            
            if (!VideoSettingPanel.Instance)
            {
                Debug.Log("해상도 설정 불러오기 실패");
            }
        }

        private void Start()
        {
            SoundSettingPanel soundPanel = FindAnyObjectByType<SoundSettingPanel>(FindObjectsInactive.Include);
            if (soundPanel != null)
            {
                soundPanel.ApplySavedVolumeSettings();
            }
            else
            {
                Debug.LogWarning("SoundSettingPanel Instance doesn't not exist. Check to SoundSettingPanel object is exist.");
            }
        }

        //[SerializeField] private SoundSettingPanel _soundSettingPanel;
        //
        //private void Start()
        //{ 
        //   if (_soundSettingPanel == null)
        //      Debug.LogError("연결하세요");
        //   
        //   _soundSettingPanel.LoadVolumeSettings();
        //   VideoSettingPanel videoSettingPanel = gameObject.AddComponent<VideoSettingPanel>();
        //   videoSettingPanel.LoadVideoSettingsDelayed();
        //}
    }
}
