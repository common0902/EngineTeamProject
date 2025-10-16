using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _00.Work.Yeonwoo._01.Scripts.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class VideoSettingPanel : Data.MonoSingletonUI<VideoSettingPanel>, IMenuPanel
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private CanvasScaler _canvasScaler;

    private static Resolution[] _resolutions;
    private static int _currentResolutionIndex;

    private const string ResolutionIndexKey = "Video_Resolution_Index";
    private const string FullscreenKey = "Video_Fullscreen";

    public bool IsOpen => gameObject.activeSelf;

    protected override void Awake()
    {
        InitializeResolutions();
        StartCoroutine(LoadVideoSettingsDelayed());

        _resolutionDropdown.onValueChanged.AddListener(SetResolution);
        _fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }
    
    public static void ApplySavedSettings()
    {
        int savedResolutionIndex = PlayerPrefs.GetInt("Video_Resolution_Index", -1);
        bool isFullscreen = PlayerPrefs.GetInt("Video_Fullscreen", 1) == 1;

        Resolution[] resolutions = Screen.resolutions;

        if (savedResolutionIndex >= 0 && savedResolutionIndex < resolutions.Length)
        {
            Resolution res = resolutions[savedResolutionIndex];
            Screen.SetResolution(res.width, res.height, isFullscreen);
        }
        else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, isFullscreen);
        }

        Screen.fullScreen = isFullscreen;
    }

    private void InitializeResolutions()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = $"{_resolutions[i].width} x {_resolutions[i].height} ({_resolutions[i].refreshRateRatio}Hz)";
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public IEnumerator LoadVideoSettingsDelayed()
    {
        yield return null;

        int resolutionIndex;
        bool isFullscreen;

        if (PlayerPrefs.HasKey(ResolutionIndexKey))
        {
            resolutionIndex = Mathf.Clamp(PlayerPrefs.GetInt(ResolutionIndexKey, _resolutions.Length - 1), 0, _resolutions.Length - 1);
        }
        else
        {
            resolutionIndex = FindClosestResolutionIndex(Screen.currentResolution.width, Screen.currentResolution.height);
        }

        if (PlayerPrefs.HasKey(FullscreenKey))
        {
            isFullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
        }
        else
        {
            isFullscreen = Screen.fullScreen;
        }

        _resolutionDropdown.value = resolutionIndex;
        _resolutionDropdown.RefreshShownValue();
        _fullscreenToggle.isOn = isFullscreen;

        SetResolution(resolutionIndex);
        SetFullscreen(isFullscreen);

        yield return new WaitForEndOfFrame();

        RefreshCanvasAndCamera();
    }

    private static int FindClosestResolutionIndex(int targetWidth, int targetHeight)
    {
        _resolutions ??= Screen.resolutions;

        int closestIndex = 0;
        int minDifference = int.MaxValue;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            int diff = Mathf.Abs(_resolutions[i].width - targetWidth) + Mathf.Abs(_resolutions[i].height - targetHeight);
            if (diff < minDifference)
            {
                minDifference = diff;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        _currentResolutionIndex = resolutionIndex;
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(ResolutionIndexKey, resolutionIndex);

        RefreshCanvasAndCamera();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);

        RefreshCanvasAndCamera();
    }

    private void RefreshCanvasAndCamera()
    {
        if (_canvasScaler == null) return;

        _canvasScaler.enabled = false;
        _canvasScaler.enabled = true;

        float targetAspect = 1920f / 1080f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (Camera.main != null)
        {
            Camera.main.rect = scaleHeight < 1f
                ? new Rect(0, (1f - scaleHeight) / 2f, 1, scaleHeight)
                : new Rect((1f - 1f / scaleHeight) / 2f, 0, 1f / scaleHeight, 1);
        }
    }

    public void Open() => gameObject.SetActive(true);

    public void Close()
    {
        gameObject.SetActive(false);
        PlayerPrefs.Save();
    }
}

}
