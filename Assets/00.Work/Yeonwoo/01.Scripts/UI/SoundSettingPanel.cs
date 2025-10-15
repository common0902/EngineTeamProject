using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SoundSettingPanel : MonoSingleton<SoundSettingPanel>, IMenuPanel
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _vfxSlider;
        
        public bool IsOpen => gameObject.activeSelf;

        private const string MasterKey = "Volume_Master";
        private const string SfxKey = "Volume_SFX";
        private const string VFXKey = "Volume_VFX";

        protected override void Awake()
        {
            Time.timeScale = 1f;
            
            _masterSlider.onValueChanged.AddListener(SetMasterVolume);
            _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
            _vfxSlider.onValueChanged.AddListener(SetVfxVolume);
            
            LoadVolumeSettings();
        }

        private void Start()
        {
            ApplyVolume(_masterSlider.value, "Master");
            ApplyVolume(_sfxSlider.value, "SFX");
            ApplyVolume(_vfxSlider.value, "VFX");
        }
        
        private void ApplyVolume(float value, string mixerParam)
        {
            float volume = Mathf.Clamp(value, 0.0001f, 1f);
            _audioMixer.SetFloat(mixerParam, Mathf.Log10(volume) * 20);
        }
        
        private void SetMasterVolume(float value)
        {
            ApplyVolume(value, "Master");
            PlayerPrefs.SetFloat(MasterKey, value);
        }
        
        private void SetSfxVolume(float value)
        {
            ApplyVolume(value, "SFX");
            PlayerPrefs.SetFloat(SfxKey, value);
        }
        
        private void SetVfxVolume(float value)
        {
            ApplyVolume(value, "VFX");
            PlayerPrefs.SetFloat(VFXKey, value);
        }
        
        private void LoadVolumeSettings()
        {
            float master = PlayerPrefs.GetFloat(MasterKey, 1f);
            float sfx = PlayerPrefs.GetFloat(SfxKey, 1f);
            float vfx = PlayerPrefs.GetFloat(VFXKey, 1f);

            _masterSlider.value = master;
            _sfxSlider.value = sfx;
            _vfxSlider.value = vfx;
        }
        
        public void Open() => gameObject.SetActive(true);
        public void Close()
        {
            gameObject.SetActive(false);
            PlayerPrefs.Save();
        }
    }
}
