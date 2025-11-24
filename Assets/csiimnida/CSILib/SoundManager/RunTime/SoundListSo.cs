using System.Collections.Generic;
using CSILib.SoundManager.RunTime;
using UnityEngine;

namespace csiimnida.CSILib.SoundManager.RunTime
{
    [CreateAssetMenu(fileName = "SoundListSO", menuName = "SO/Sound/SoundListSO")]
    public class SoundListSo : ScriptableObject
    {
        [SerializeField] private List<SoundSo> Sounds = new List<SoundSo>();

        public Dictionary<string, SoundSo> SoundsDictionary;

        private void OnEnable()
        {
			if(Sounds == null)
				return;
            SoundsDictionary = new Dictionary<string, SoundSo>();
            foreach (SoundSo soundSo in Sounds)
            {
                SoundsDictionary[soundSo.soundName] = soundSo;
            }
        }
        public void AddSound(SoundSo soundSo)
        {
            Sounds.Add(soundSo);
        }

        public List<SoundSo> GetSoundList() => Sounds;

        public void RemoveSound(SoundSo so)
        {
            if (so != null)
            {
                Sounds.Remove(so);
            }
        }
    }
}
