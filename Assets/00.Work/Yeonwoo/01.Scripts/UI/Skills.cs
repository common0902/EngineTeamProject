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
            icon.sprite = skillData.Icon;
            nameText.text = skillData.Name;
            descriptionText.text = skillData.Description;
            statText.text = $"공격력: {skillData.Stat}";
            consumptionText.text = $"소모값: {skillData.Consumption}";
        }
    }
}