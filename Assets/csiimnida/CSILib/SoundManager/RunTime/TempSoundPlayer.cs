using UnityEngine;

namespace csiimnida.CSILib.SoundManager.RunTime
{
    public class TempSoundPlayer : MonoBehaviour
    {
        [SerializeField] private string soundName;
        private void Start()
        {
            csiimnida.CSILib.SoundManager.RunTime.SoundManager.Instance.PlaySound(soundName);
        }

    }
}