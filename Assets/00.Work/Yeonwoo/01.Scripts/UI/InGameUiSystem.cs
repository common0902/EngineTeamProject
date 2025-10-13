using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class InGameUiSystem : UISystem
    {
        protected override void TogglePanel()
        {
            base.TogglePanel();
            Time.timeScale = isActive ? 1 : 0;
        }
    }
}