using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class PlayerStateStopper : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] controlScripts;
        
        public void DisableControls()
        {
            foreach (MonoBehaviour script in controlScripts)
                script.enabled = false;
        }

        public void EnableControls()
        {
            foreach (MonoBehaviour script in controlScripts)
                script.enabled = true;
        }
    }
}