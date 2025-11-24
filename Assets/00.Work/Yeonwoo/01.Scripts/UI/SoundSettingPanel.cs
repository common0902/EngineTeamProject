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
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;
        
        public bool IsOpen => gameObject.activeSelf;

        private const string MasterKey = "Volume_Master";
        private const string BgmKey = "Volume_BGM";
        private const string SfxKey = "Volume_SFX";
        
        private const string MasterVolumeKey = "Master";
        private const string BgmVolumeKey = "BGM";
        private const string SfxVolumeKey = "SFX";
        
        protected override void Awake()
        {
            base.Awake();
            
            if (_masterSlider != null) _masterSlider.onValueChanged.AddListener(SetMasterVolume);
            else Debug.LogWarning("[SoundSettingPanel] _masterSlider is not assigned.");

            if (_bgmSlider != null) _bgmSlider.onValueChanged.AddListener(SetBgmVolume);
            else Debug.LogWarning("[SoundSettingPanel] _sfxSlider is not assigned.");

            if (_sfxSlider != null) _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
            else Debug.LogWarning("[SoundSettingPanel] _vfxSlider is not assigned.");
            
            LoadVolumeSettings();
        }
        
        private void SetMasterVolume(float value)
        {
            PlayerPrefs.SetFloat(MasterKey, value);
            SetAudioMixerVolume(MasterVolumeKey, value);
        }

        private void SetBgmVolume(float value)
        {
            PlayerPrefs.SetFloat(BgmKey, value);
            SetAudioMixerVolume(BgmVolumeKey, value);
        }

        private void SetSfxVolume(float value)
        {
            PlayerPrefs.SetFloat(SfxKey, value);
            SetAudioMixerVolume(SfxVolumeKey, value);
        }

        private void SetAudioMixerVolume(string paraName, float value)
        {
            if (_audioMixer == null) return;

            float dB = value <= 0.0001f ? -80f : Mathf.Log10(value) * 20f;
            _audioMixer.SetFloat(paraName, dB);
        }
        
        private void LoadVolumeSettings()
        {
            float master = PlayerPrefs.GetFloat(MasterKey, 1f);
            float sfx = PlayerPrefs.GetFloat(BgmKey, 1f);
            float vfx = PlayerPrefs.GetFloat(SfxKey, 1f);

            _masterSlider.value = master;
            _bgmSlider.value = sfx;
            _sfxSlider.value = vfx;
            
            SetAudioMixerVolume(MasterVolumeKey, master);
            SetAudioMixerVolume(BgmVolumeKey, sfx);
            SetAudioMixerVolume(SfxVolumeKey, vfx);
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
