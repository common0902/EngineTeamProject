using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class DashCoolUI : MonoBehaviour
    {
        [field:SerializeField] public Image CoolTimeImage { get; private set; }
        
        public void SetFill(float normalized)
        {
            if (CoolTimeImage == null) return;
            CoolTimeImage.fillAmount = Mathf.Clamp01(normalized);
        }
    }
}