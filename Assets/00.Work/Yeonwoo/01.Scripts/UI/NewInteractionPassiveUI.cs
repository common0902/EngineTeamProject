using System;
using System.Net;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class NewInteractionPassiveUI : AbstractInteractionUI
    {
        private PassiveData _currentSkillData;
        
        public event Action OnShow;
        public event Action OnHide;
        
        private void Start()
        {
            Hide();
        }

        //public void Show(PassiveData sd)
        //{
        //    if (sd == null) return;

        //    if (staminaText == null)
        //        Debug.Log("상관 X");
        //    _currentSkillData = sd;
        //    if (icon != null && sd.Icon != null) icon.sprite = sd.Icon;
        //    if (nameText != null) nameText.text = string.IsNullOrEmpty(sd.Name) ? "NO NAME" : sd.Name;
        //    if (descriptionText != null) descriptionText.text = string.IsNullOrEmpty(sd.Description) ? "-" : sd.Description;
        //    if (statText != null) statText.text = string.IsNullOrEmpty(sd.Stat) ? "-" : $"사용 효과: {sd.Stat}";

        //    gameObject.SetActive(true);
        //    OnShow?.Invoke();
        //}
        public void Show(PassiveSkill potion)
        {
            if (potion == null) return;

            if (staminaText == null)
                Debug.Log("상관 X");
            _currentSkillData = potion.Data;
            if (icon != null && potion.Data.Icon != null) icon.sprite = potion.Data.Icon;
            if (nameText != null) nameText.text = string.IsNullOrEmpty(potion.Data.Name) ? "NO NAME" : potion.Data.Name;
            if (descriptionText != null) descriptionText.text = string.IsNullOrEmpty(potion.Data.Description) ? "-" : potion.Data.Description;
            if (statText != null) statText.text = string.IsNullOrEmpty(potion.Data.Stat) ? "-" : $"사용 효과: {potion.Data.Stat}{potion._statusValue}";

            gameObject.SetActive(true);
            OnShow?.Invoke();
        }

        public void Hide()
        {
            _currentSkillData = null;
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }
        
        public bool IsShowing(PassiveData sd)
        {
            return _currentSkillData == sd && gameObject.activeSelf;
        }
    }
}