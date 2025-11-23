using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class NewInteractionSkillUI : AbstractInteractionUI
    {
        private SkillData _currentSkillData;
        
        public event Action OnShow;
        public event Action OnHide;
        
        private void Start()
        {
            Hide();
        }

        public void Show(SkillData sd)
        {
            if (sd == null) return;

            _currentSkillData = sd;
            if (icon != null && sd.Icon != null) icon.sprite = sd.Icon;
            if (nameText != null) nameText.text = string.IsNullOrEmpty(sd.Name) ? "DataError" : sd.Name;
            if (descriptionText != null) descriptionText.text = string.IsNullOrEmpty(sd.Description) ? "-" : sd.Description;
            if (statText != null) statText.text = string.IsNullOrEmpty(sd.Stat) ? "-" : $"효과: {sd.Stat}";
            if (staminaText != null) staminaText.text = string.IsNullOrEmpty(sd.Consumption) ? "-" : $"소모값: {sd.Consumption}";

            gameObject.SetActive(true);
            OnShow?.Invoke();
        }

        public void Hide()
        {
            _currentSkillData = null;
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }

        public bool IsShowing(SkillData sd)
        {
            return _currentSkillData == sd && gameObject.activeSelf;
        }
    }
}