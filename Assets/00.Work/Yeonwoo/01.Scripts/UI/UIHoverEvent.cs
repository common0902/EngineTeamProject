using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class UIHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioClip _audioClip;
        private AudioSource _audioSource;

        private Vector3 _originScale;
        
        private void Awake()
        {
            if (!_audioClip) 
                Debug.Log("호버 오디오 안 넣음");
            
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
                _audioSource = gameObject.AddComponent<AudioSource>();
        
            if (_audioMixerGroup != null)
                _audioSource.outputAudioMixerGroup = _audioMixerGroup;
        
            _audioSource.playOnAwake = false;

            _originScale = transform.localScale;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _audioSource.PlayOneShot(_audioClip);
        
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f)
                .SetUpdate(true);
        }
    
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f)
                .SetUpdate(true);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
            this.transform.localScale = _originScale;
        }
    }
}
