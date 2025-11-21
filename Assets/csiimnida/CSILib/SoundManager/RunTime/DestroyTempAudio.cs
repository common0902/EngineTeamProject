using System;
using UnityEngine;
namespace csiimnida.CSILib.SoundManager.RunTime
{
    public class DestroyTempAudio : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject);
        }
    }
}