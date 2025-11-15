using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class Skills : AbstractSkills
    {
        [SerializeField] private SkillData skillData;
        [SerializeField] private NewInteractionSkillUI newInteractionSkillUI;
        
        protected override void Awake()
        {
            base.Awake();
            if (newInteractionSkillUI == null)
                Debug.LogError("No Interaction New Skill UI Found");
        }

        protected override void OnPlayerEnterRange()
        {
            if (newInteractionSkillUI == null || skillData == null) return;
            
            if (!newInteractionSkillUI.IsShowing(skillData))
            {
                newInteractionSkillUI.Show(skillData);
            }
        }

        protected override void OnPlayerExitRange()
        {
            if (newInteractionSkillUI == null || skillData == null) return;

            if (newInteractionSkillUI.IsShowing(skillData))
            {
                newInteractionSkillUI.Hide();
            }
        }
    }
}