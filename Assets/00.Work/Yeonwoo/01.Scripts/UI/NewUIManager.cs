using System;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class NewUIManager : MonoBehaviour
    {
        [field:SerializeField] public SkillUI[] SkillUI {get; private set;}
        [field:SerializeField] public PassiveSkillUI[] PassiveSkillUI  { get; private set; }
        [field:SerializeField] public SkillController SkillControllerCompo { get; private set; }

        private void Start()
        {
            SkillControllerCompo.OnChangeSkill += UpdateSkillUI;
        }

        private void UpdateSkillUI()
        {
            Skill[] elements = SkillControllerCompo.Skills.ToArray();
            int j = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (elements[i] == null)
                {
                    // 코드 어떻게 짜지
                    //SkillUI[i].NameText = SkillUI[j].NameText;
                    //j++;
                    //continue;
                }
                SkillUI[i].NameText = SkillUI[j].NameText;
                j++;
            }
        }
    }
}