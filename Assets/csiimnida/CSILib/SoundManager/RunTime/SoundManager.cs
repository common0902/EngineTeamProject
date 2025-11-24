using System.Collections;
using System.Collections.Generic;
using CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.Audio;

namespace csiimnida.CSILib.SoundManager.RunTime
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
    
        [SerializeField] private SoundListSo _soundListSo;
        [SerializeField] private AudioMixer _mixer;
        private Dictionary<string, List<AudioSource>> _playingSounds = new Dictionary<string, List<AudioSource>>();
        private void Awake()
        {
            if (_soundListSo == null)
            {
                Debug.Assert(_soundListSo != null,$"SoundListSo asset is null");
            }
            if (_mixer == null)
            {
                Debug.LogError("AudioMixer가 할당되지 않았습니다. SoundManager를 사용하기 전에 할당해주세요.");
            }
        }
        public void PlaySound(string soundName)
        {
            GameObject obj = new GameObject();
            obj.name = soundName + " Sound";
            AudioSource source = obj.AddComponent<AudioSource>();
            SoundSo so = _soundListSo.SoundsDictionary[soundName];
            if (_mixer == null)
            {
                Debug.LogWarning("Mixer가 할당되지 않았습니다. SoundManager를 사용하기 전에 할당해주세요.");
                SetAudio(source,so,soundName);
                return;
            }
            if(so.soundType == SoundType.SFX)
                source.outputAudioMixerGroup = _mixer.FindMatchingGroups("SFX")[0];
            else if(so.soundType == SoundType.BGM)
            {
                source.outputAudioMixerGroup = _mixer.FindMatchingGroups("BGM")[0];
            }
            else
            {
                Debug.LogWarning("Type이 없습니다");
                source.outputAudioMixerGroup = _mixer.FindMatchingGroups("Master")[0];

            }
            SetAudio(source,so,soundName);
        
        }

        private void SetAudio(AudioSource source, SoundSo sounds, string soundName)
        {
            source.clip = sounds.clip;
            source.loop = sounds.loop;
            source.priority = sounds.Priority;
            source.volume = sounds.volume;
            source.pitch = sounds.pitch;
            source.panStereo = sounds.stereoPan;
            source.spatialBlend = sounds.SpatialBlend;
            if (sounds.RandomPitch)
            {
                sounds.pitch = Random.Range(sounds.MinPitch, sounds.MaxPitch);
            }
            if (sounds.pitch < 0)
            {
                source.time = 1;
            }
            if (!_playingSounds.ContainsKey(soundName))
            {
                _playingSounds[soundName] = new List<AudioSource>();
            }
            _playingSounds[soundName].Add(source);
            source.Play();
            if (!sounds.loop) { StartCoroutine(DestroyCo(source.clip.length,source.gameObject)); }

        }

        [ContextMenu("Stop Sound")]
        public void MainGameBGMStop()
        {
            StopSound("MainGameBGM");
        }
        // 특정 사운드 전부 멈추기
        public void StopSound(string soundName)
        {
            if (_playingSounds.ContainsKey(soundName))
            {
                foreach (var source in _playingSounds[soundName])
                {
                    if (source != null)
                    {
                        source.Stop();
                        Destroy(source.gameObject);
                    }
                }
                _playingSounds[soundName].Clear();
            }
        }
        // 특정 사운드 하나만 멈추기 (가장 최근 재생)
        public void StopSoundOne(string soundName)
        {
            if (_playingSounds.ContainsKey(soundName) && _playingSounds[soundName].Count > 0)
            {
                var lastIndex = _playingSounds[soundName].Count - 1;
                var source = _playingSounds[soundName][lastIndex];
                
                if (source != null)
                {
                    source.Stop();
                    Destroy(source.gameObject);
                }
                
                _playingSounds[soundName].RemoveAt(lastIndex);
            }
        }
        
        public void StopAllSounds()
        {
            foreach (var soundList in _playingSounds.Values)
            {
                foreach (var source in soundList)
                {
                    if (source != null)
                    {
                        source.Stop();
                        Destroy(source.gameObject);
                    }
                }
            }
            _playingSounds.Clear();
        }
        
        public void StopSoundsByType(SoundType soundType)
        {
            List<string> soundsToStop = new List<string>();
            
            foreach (var kvp in _playingSounds)
            {
                if (_soundListSo.SoundsDictionary.ContainsKey(kvp.Key))
                {
                    if (_soundListSo.SoundsDictionary[kvp.Key].soundType == soundType)
                    {
                        soundsToStop.Add(kvp.Key);
                    }
                }
            }
            
            foreach (var soundName in soundsToStop)
            {
                StopSound(soundName);
            }
        }

        IEnumerator DestroyCo(float endTime,GameObject obj)
        {
            yield return new WaitForSecondsRealtime(endTime);
            Destroy(obj);
        }
    }

    public enum SoundType
    {
        BGM,
        SFX
    }
}