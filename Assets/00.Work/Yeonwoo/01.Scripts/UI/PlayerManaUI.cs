using System;
using _00.Work.SYH._02Script.ETG;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PlayerManaUI : MonoBehaviour
    {
        private PlayerStatus _mana;
        private Slider _manaBar;
        private TextMeshProUGUI  _manaText;
        private Tween _blinkTween;
        private CanvasGroup _canvasGroup;
        private HealthSystem  _healthSystem;
        [SerializeField] private Image warningImage;
        private float _displayMana;
        
        private void Awake()
        {
            _healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
            _mana = GameObject.Find("Player").GetComponent<PlayerStatus>();
            _manaBar = GetComponent<Slider>();
            _manaText = GetComponentInChildren<TextMeshProUGUI>();
            
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
                Debug.LogError("Canvasgroup is null");
        }

        private void Start()
        {
            RefreshUIImmediate();
            _healthSystem.OnDead += DeadUI;
            warningImage.gameObject.SetActive(false);
        }

        private void Update()
        {
            RefreshUISmooth();
        }
        
        private void RefreshUIImmediate()
        {
            _manaText.text = $"{_mana.Mana:F0} / {_mana._fullMana:F0}";
            _manaBar.maxValue = _mana._fullMana;
            _manaBar.value = _mana.Mana;
        }
        
        private void RefreshUISmooth()
        {
            DOTween.To(
                () => _displayMana,
                x => {
                    _displayMana = x;
                    _manaText.text = $"{_displayMana:F0} / {_mana._fullMana:F0}";
                },
                _mana.Mana,
                0.2f
            ).SetEase(Ease.OutQuad);
            
            _manaBar.maxValue = _mana._fullMana;
            _manaBar.DOValue(_mana.Mana, 0.25f).SetEase(Ease.OutQuad);
        }

        public void ManaZeroAnim()
        {
            warningImage.gameObject.SetActive(true);
            _blinkTween?.Kill();
            
            _blinkTween = _canvasGroup
                .DOFade(0.01f, 0.8f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
            Debug.Log("마나없음");
        }

        public void ManaRecoverAnim()
        {
            warningImage.gameObject.SetActive(false);
            _blinkTween?.Kill();
            _canvasGroup.DOFade(1f, 0.2f);
            Debug.Log("마나복구");
        }

        private void DeadUI()
        {
            _canvasGroup.DOFade(0f, 0.5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            _healthSystem.OnDead -= DeadUI;
        }
    }
}