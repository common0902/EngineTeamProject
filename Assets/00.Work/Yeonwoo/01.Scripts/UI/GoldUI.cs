using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class GoldUI : MonoBehaviour
    {
       private TextMeshProUGUI  _text;
        private GoldSystem _goldSystem;

        private int _currentDisplayGold;
        private Tween _goldTween;
        
        [SerializeField] private float tweenDuration = 0.3f;
        
        private void Awake()
        {
            _goldSystem = GetComponent<GoldSystem>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            if (_goldSystem != null)
            {
                _goldSystem.OnGoldChanged += GoldUIChange;
            }
        }
        
        private void Start()
        {
            if (_goldSystem == null || _text == null) return;
            
            _currentDisplayGold = _goldSystem.Gold;
            _text.text = $"{_currentDisplayGold}";
        }
        
        private void GoldUIChange()
        {
            if (_goldSystem == null || _text == null) return;

            int targetGold = _goldSystem.Gold;
            
            if (_goldTween != null && _goldTween.IsActive())
            {
                _goldTween.Kill();
            }

            int startValue = _currentDisplayGold;

            _goldTween = DOTween.To(
                    () => startValue,
                    x =>
                    {
                        startValue = x;
                        _currentDisplayGold = x;
                        _text.text = $"{_currentDisplayGold}";
                    },
                    targetGold,
                    tweenDuration
                )
                .SetEase(Ease.OutQuad);
        }

        private void OnDestroy()
        {
            if (_goldSystem != null)
            {
                _goldSystem.OnGoldChanged -= GoldUIChange;
            }

            if (_goldTween != null && _goldTween.IsActive())
            {
                _goldTween.Kill();
            }
        }

    }
}