using System;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

namespace csiimnida.CSILib.SoundManager.RunTime
{
    public class SoundSo : ScriptableObject
    {
        public string soundName;
        [Space(5)]
        public SoundType soundType = SoundType.SFX;
    
        [Space(3)]
        public AudioClip clip;
    
        [Space(6)]
        public bool loop;
    
        [Space(2)]
        [Range(0f,256f)] [Tooltip("장면에 공존하는 모든 오디오 소스 중에서 이 오디오 소스의 우선순위를 결정합니다. (우선순위: 0 = 가장 중요함. 256 = 가장 덜 중요함. 기본값 = 128.). 음악 트랙에는 0을 사용하여 가끔씩 바뀌는 것을 방지합니다.")]
        public int Priority = 128;
    
        [Space(2)]
        [Range(0f,1f)] [Tooltip("1월드 단위(1미터) 떨어진 곳에서 소리가 얼마나 큰가오디오 리스너\n.")]
        public float volume = 1.0f;

        [Space(2)]
        [Range(-3f,3f)][Tooltip("오디오 클립 의 감속/가속으로 인한 피치 변화량 . 값 1은 일반 재생 속도입니다.")]
        public float pitch = 1.0f;
    
        [Space(2)]
        [Range(-1f,1f)] [Tooltip("2D 사운드의 스테레오 필드에서 위치를 설정합니다.")]
        public float stereoPan = 0.0f;
    
        [Space(2)]
        [Range(0f,1f)] [Tooltip("3D 엔진이 오디오 소스에 얼마나 많은 영향을 미치는지 설정합니다.")]
        public float SpatialBlend = 0.0f;
        
        
        public bool RandomPitch = false;
        public float MinPitch = 0.95f;
        public float MaxPitch = 1.05f;


    }
    
}
    
