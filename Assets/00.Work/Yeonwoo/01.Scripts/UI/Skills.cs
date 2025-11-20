using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class Skills : AbstractSkills
    {
        [SerializeField] private SkillData skillData;
        private NewInteractionSkillUI _newInteractionSkillUI;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _newInteractionSkillUI = FindAnyObjectByType<NewInteractionSkillUI>(FindObjectsInactive.Include);
            if (_newInteractionSkillUI == null)
                Debug.LogWarning("No Interaction New Skill UI Found");
        }

        protected override void OnPlayerEnterRange()
        {
            if (_newInteractionSkillUI == null || skillData == null) return;
            
            if (!_newInteractionSkillUI.IsShowing(skillData))
            {
                _newInteractionSkillUI.Show(skillData);
            }
        }

        protected override void OnPlayerExitRange()
        {
            if (_newInteractionSkillUI == null || skillData == null) return;

            if (_newInteractionSkillUI.IsShowing(skillData))
            {
                _newInteractionSkillUI.Hide();
            }
        }
    }
}