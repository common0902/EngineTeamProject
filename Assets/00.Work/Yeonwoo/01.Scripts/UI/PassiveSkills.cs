using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PassiveSkills : AbstractSkills
    {
        [SerializeField] private PassiveData passiveData;
        
        protected override void Awake()
        { 
            base.Awake();
            icon.sprite = passiveData.Icon;
            nameText.text = passiveData.Name;
            descriptionText.text = passiveData.Description;
            statText.text = $"공격력: {passiveData.Stat}";
            consumptionText.text = $"소모값: {passiveData.Consumption}";
        }
    }
}