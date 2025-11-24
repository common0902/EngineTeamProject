using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class OldInteractionSkillUI : AbstractInteractionUI
    {
        private SkillData _current;

        public void Show(SkillData sd)
        {
            if (sd == null) return;
            _current = sd;
            if (icon != null && sd.Icon != null) icon.sprite = sd.Icon;
            if (nameText != null) nameText.text = string.IsNullOrEmpty(sd.Name) ? "NO NAME" : sd.Name;
            if (descriptionText != null) descriptionText.text = string.IsNullOrEmpty(sd.Description) ? "-" : sd.Description;
            if (statText != null) statText.text = string.IsNullOrEmpty(sd.Stat) ? "-" : $"공격력: {sd.Stat}";
            if (staminaText != null) staminaText.text = string.IsNullOrEmpty(sd.Consumption) ? "-" : $"소모값: {sd.Consumption}";

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _current = null;
            gameObject.SetActive(false);
        }
    }
}