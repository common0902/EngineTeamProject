using System;
using UnityEngine;
using UnityEngine.Audio;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SoundSystem : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioClip _audioClip;
    
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
                _audioSource = gameObject.AddComponent<AudioSource>();
        
            if (_audioMixerGroup != null)
                _audioSource.outputAudioMixerGroup = _audioMixerGroup;
        
            _audioSource.playOnAwake = false;
        }

        public void ButtonClick()
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}