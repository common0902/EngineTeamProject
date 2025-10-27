using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class Skills : AbstractSkills
    {
        [SerializeField] private SkillData skillData;

        protected override void Awake()
        {
            base.Awake();
            nameText.text = skillData.Name;
            descriptionText.text = skillData.Description;
        }
        
        protected override void Interaction()
        {
            base.Interaction();
        }
    }
}