using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SoundSettingPanel : Data.MonoSingletonUI<SoundSettingPanel>, IMenuPanel
    {
        [Header("음향 시스템")]
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _vfxSlider;

        [Header("음소거 토글")]
        [SerializeField] private Toggle _masterMuteToggle;
        [SerializeField] private Toggle _sfxMuteToggle;
        [SerializeField] private Toggle _vfxMuteToggle;
        
        public bool IsOpen => gameObject.activeSelf;

        private const string MasterKey = "Volume_Master";
        private const string SfxKey = "Volume_SFX";
        private const string VfxKey = "Volume_VFX";
        private const string MasterMuteKey = "Mute_Master";
        private const string SfxMuteKey = "Mute_SFX";
        private const string VfxMuteKey = "Mute_VFX";

        protected override void Awake()
        {
            _masterSlider.onValueChanged.AddListener(SetMasterVolume);
            _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
            _vfxSlider.onValueChanged.AddListener(SetVfxVolume);
            
            LoadVolumeSettings();
            LoadMuteSettings();
            
            ApplyAllVolumes();
        }

        private void Start()
        {
            ApplyAllVolumes();
        }

        private void ApplyAllVolumes()
        {
            ApplyVolume(_masterSlider.value, "Master", _masterMuteToggle.isOn);
            ApplyVolume(_sfxSlider.value, "SFX", _sfxMuteToggle.isOn);
            ApplyVolume(_vfxSlider.value, "VFX", _vfxMuteToggle.isOn);
        }

        private void ApplyVolume(float value, string mixerParam, bool isMuted)
        {
            isMuted = !isMuted;
            float volume = Mathf.Clamp(value, 0.0001f, 1f);
            float dB = isMuted ? -80f : Mathf.Log10(volume) * 20;
            _audioMixer.SetFloat(mixerParam, dB);
        }
        
        private void SetMasterVolume(float value)
        {
            ApplyVolume(value, "Master", _masterMuteToggle.isOn);
            PlayerPrefs.SetFloat(MasterKey, value);
        }

        private void SetSfxVolume(float value)
        {
            ApplyVolume(value, "SFX", _sfxMuteToggle.isOn);
            PlayerPrefs.SetFloat(SfxKey, value);
        }

        private void SetVfxVolume(float value)
        {
            ApplyVolume(value, "VFX", _vfxMuteToggle.isOn);
            PlayerPrefs.SetFloat(VfxKey, value);
        }
        
        public void SetMasterMute(bool isMuted)
        {
            ApplyVolume(_masterSlider.value, "Master", isMuted);
            PlayerPrefs.SetInt(MasterMuteKey, isMuted ? 1 : 0);
        }

        public void SetSfxMute(bool isMuted)
        {
            ApplyVolume(_sfxSlider.value, "SFX", isMuted);
            PlayerPrefs.SetInt(SfxMuteKey, isMuted ? 1 : 0);
        }

        public void SetVfxMute(bool isMuted)
        {
            ApplyVolume(_vfxSlider.value, "VFX", isMuted);
            PlayerPrefs.SetInt(VfxMuteKey, isMuted ? 1 : 0);
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

        private void LoadMuteSettings()
        {
            bool masterMute = PlayerPrefs.GetInt(MasterMuteKey, 0) == 1;
            bool sfxMute = PlayerPrefs.GetInt(SfxMuteKey, 0) == 1;
            bool vfxMute = PlayerPrefs.GetInt(VfxMuteKey, 0) == 1;

            _masterMuteToggle.isOn = masterMute;
            _sfxMuteToggle.isOn = sfxMute;
            _vfxMuteToggle.isOn = vfxMute;
        }

        public void ApplySavedVolumeSettings()
        {
            LoadVolumeSettings();
            LoadMuteSettings();
            ApplyAllVolumes();
        }

        public void Open() => gameObject.SetActive(true);

        public void Close()
        {
            gameObject.SetActive(false);
            PlayerPrefs.Save();
        }
    }
}
