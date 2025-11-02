using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class NewUIManager : MonoBehaviour
    {
        [field: SerializeField] public SkillUI[] SkillUI { get; private set; }
        [field: SerializeField] public PassiveSkillUI[] PassiveSkillUI { get; private set; }
        
        [field: SerializeField] public SkillController SkillControllerCompo { get; private set; }
      //  [field: SerializeField] public PassiveSkillController PassiveControllerCompo { get; private set; }

        private void Start()
        {
            SkillControllerCompo.OnChangeSkill += UpdateSkillUI;
          //  PassiveControllerCompo.OnChangePassive += UpdatePassiveSkillUI;
        }
        
        // 현재 보유 중인 스킬 리스트를 받아 UI 슬롯에 반영
        private void UpdateSkillUI()
        {
            var elements = SkillControllerCompo.Skills.ToArray();

            for (int i = 0; i < SkillUI.Length; i++)
            {
                // 슬롯이 비어있거나 null이면 초기화, 있으면 SkillData 전달
                if (i < elements.Length && elements[i] != null)
                //    SkillUI[i].SetSkillData(elements[i].Data);
          //      else
                    SkillUI[i].SetSkillData(null);
            }
        }
        
        // 위랑 똑같은 패시브 UI 갱신
        private void UpdatePassiveSkillUI()
        {
           // var elements = PassiveControllerCompo.Passives.ToArray();

            for (int i = 0; i < PassiveSkillUI.Length; i++)
            {
         //       if (i < elements.Length && elements[i] != null)
              //      PassiveSkillUI[i].SetPassiveData(elements[i].Data);
               // else
                    PassiveSkillUI[i].SetPassiveData(null);
            }
        }

        private void OnDisable()
        {
            SkillControllerCompo.OnChangeSkill -= UpdateSkillUI;
           // PassiveControllerCompo.OnChangePassive -= UpdatePassiveSkillUI;
        }
    }
}