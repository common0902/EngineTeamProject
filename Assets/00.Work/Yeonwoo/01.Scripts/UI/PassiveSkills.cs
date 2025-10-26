using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PassiveSkills : AbstractSkills
    {
        [SerializeField] private PassiveData passiveData;

        protected override void Awake()
        {
            base.Awake();
            nameText.text = passiveData.Name;
            descriptionText.text = passiveData.Description;
        }

        protected override void Interaction()
        {
            base.Interaction();
        }
    }
}