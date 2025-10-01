using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SettingPanel : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _vfxSlider;
    
        private static SettingPanel _instance;

        public static SettingPanel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<SettingPanel>(FindObjectsInactive.Include);
                    if (_instance == null)
                    {
                        Debug.LogError("SettingPanel Instance does not exist");
                    }
                }
                return _instance;
            }
        }

        private void SetMasterVolume(float volume)
        {
            _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        }

        private void SetSFXVolume(float volume)
        {
            _audioMixer.SetFloat("VFX", Mathf.Log10(volume) * 20);
        }

        private void SetVFXVolume(float volume)
        {
            _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }

        public void GoTitle()
        {
            SceneManager.LoadScene(0);
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.Log(
                    $"Another Instance ({_instance.gameObject.name}) already exists, Destroy This Instance({gameObject.name}).");
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
            _masterSlider.onValueChanged.AddListener(SetMasterVolume);
            _vfxSlider.onValueChanged.AddListener(SetSFXVolume);
            _sfxSlider.onValueChanged.AddListener(SetVFXVolume);
        }

        public void SettingPanelOpen()
        {
            gameObject.SetActive(true);
        }

        public void SettingPanelClose()
        {
            gameObject.SetActive(false);
        }
    }
}