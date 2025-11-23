using System;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class Fire : MonoBehaviour, IDeathCinema
    {
        private static readonly int FlameBrightness = Shader.PropertyToID("_FlameBrightness");
        private static readonly int FlameSmooth = Shader.PropertyToID("_FlameSmooth");
        private Image _image;
        private CanvasGroup _canvasGroup;
        private Material _material;

        [SerializeField] private bool destroyOnDead = true;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _material = _image != null ? _image.material : null;

            if (_material != null)
            {
                _material.SetFloat(FlameBrightness, 10f);
                _material.SetFloat(FlameSmooth, 2f);
            }
        }

        private void Start()
        {
        }

        private void OnEnable()
        {
            ResetInit();
        }

        private void ResetInit()
        {
            if (_canvasGroup != null)
                _canvasGroup.alpha = 0f;
            else if (_image != null)
            {
                Color c = _image.color;
                c.a = 0f;
                _image.color = c;
            }

            transform.localScale = Vector3.one;
        }
        
        public Tween PlayEntrance(float duration, bool useUnscaled = true)
        {
            gameObject.SetActive(true);

            Tween fadeTween;
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 0f;
                fadeTween = _canvasGroup.DOFade(1f, duration);
            }
            else if (_image != null)
            {
                Color c = _image.color;
                c.a = 0f;
                _image.color = c;
                fadeTween = _image.DOFade(1f, duration);
            }
            else
            {
                fadeTween = DOVirtual.DelayedCall(0f, () => { });
            }

            return fadeTween.SetUpdate(useUnscaled);
        }

        public Tween PlayDead(float duration, bool useUnscaled = true)
        {
            Sequence seq = DOTween.Sequence();

            if (_material != null)
            {
                seq.Append(_material.DOFloat(0f, "_FlameBrightness", duration));
                seq.Join(_material.DOFloat(0f, "_FlameSmooth", duration));
            }
            else
            {
                seq.AppendInterval(duration);
            }

            seq.OnComplete(() =>
            {
                if (destroyOnDead)
                    Destroy(gameObject);
                else
                    gameObject.SetActive(false);
            });

            return seq.SetUpdate(useUnscaled);
        }

        public Tween PlayWin(float duration, bool useUnscaled = true)
        {
            float targetScale = 1.4f;
            Sequence seq = DOTween.Sequence();
            
            if (_material != null)
            {
                seq.Append(_material.DOFloat(20f, "_FlameBrightness", duration * 0.35f));
            }
            
            seq.Append(transform.DOScale(targetScale, duration * 0.45f).SetEase(Ease.OutCubic));
            seq.Append(transform.DOScale(Vector3.one, duration * 0.2f).SetEase(Ease.OutBack));
            
            seq.SetUpdate(useUnscaled);
            return seq;
        }
    }
}