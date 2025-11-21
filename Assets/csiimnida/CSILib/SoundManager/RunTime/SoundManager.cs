using System.Collections;
using CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.Audio;

namespace csiimnida.CSILib.SoundManager.RunTime
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
    
        [SerializeField] private SoundListSo _soundListSo;
        [SerializeField] private AudioMixer _mixer;

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
                SetAudio(source,so);
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
            SetAudio(source,so);
        
        }

        private void SetAudio(AudioSource source,SoundSo sounds)
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
            source.Play();
            if (!sounds.loop) { StartCoroutine(DestroyCo(source.clip.length,source.gameObject)); }

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