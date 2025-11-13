using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SoundSettingPanel : MonoSingletonUI<SoundSettingPanel>, ISettingPanel
    {
        [Header("음향 시스템")]
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _vfxSlider;
        
        public bool IsOpen => gameObject.activeSelf;

        private const string MasterKey = "Volume_Master";
        private const string SfxKey = "Volume_SFX";
        private const string VfxKey = "Volume_VFX";
        
        protected override void Awake()
        {
            _masterSlider.onValueChanged.AddListener(SetMasterVolume);
            _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
            _vfxSlider.onValueChanged.AddListener(SetVfxVolume);
            
            LoadVolumeSettings();
        }
        
        private void SetMasterVolume(float value)
        {
            PlayerPrefs.SetFloat(MasterKey, value);
        }

        private void SetSfxVolume(float value)
        {
            PlayerPrefs.SetFloat(SfxKey, value);
        }

        private void SetVfxVolume(float value)
        {
            PlayerPrefs.SetFloat(VfxKey, value);
        }
        
        private void LoadVolumeSettings()
        {
            float master = PlayerPrefs.GetFloat(MasterKey, 1f);
            float sfx = PlayerPrefs.GetFloat(SfxKey, 1f);
            float vfx = PlayerPrefs.GetFloat(VfxKey, 1f);

            _masterSlider.value = master;
            _sfxSlider.value = sfx;
            _vfxSlider.value = vfx;
        }

        
        public void ApplySavedVolumeSettings()
        {
            LoadVolumeSettings();
        }

        public void Open() => gameObject.SetActive(true);

        public void Close()
        {
            gameObject.SetActive(false);
            PlayerPrefs.Save();
        }
    }
}
