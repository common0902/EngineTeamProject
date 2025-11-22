using System;
using _00.Work.Yeonwoo._01.Scripts.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class TitleFire : MonoBehaviour
    {
        [SerializeField] private SettingSystem settingSystem;
        private RectTransform _thisTransform;

        private void Awake()
        {
            _thisTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            settingSystem.OnStartButtonClick += StartEvent;
        }

        private void StartEvent()
        {
            _thisTransform.DOSizeDelta(new Vector2(8500f, 8500f), 3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene("Prolog");
                });
        }
        
        private void OnDisable()
        {
            settingSystem.OnStartButtonClick -= StartEvent;
        }
    }
}