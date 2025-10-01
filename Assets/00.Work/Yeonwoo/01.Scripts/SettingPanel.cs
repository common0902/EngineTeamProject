using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    //[SerializeField] private AudioMixer _audioMixer;
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