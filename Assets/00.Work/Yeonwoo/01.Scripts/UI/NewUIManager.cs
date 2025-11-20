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
        [field: SerializeField] public PassiveSkillController PassiveControllerCompo { get; private set; }
        
        [SerializeField] private NewInteractionSkillUI newInteractionSkillUI;
        [SerializeField] private OldInteractionSkillUI oldInteractionSkillUI;
        [SerializeField] private NewInteractionPassiveUI newInteractionPassiveUI;

        private void Start()
        {
            SkillControllerCompo.OnChangeSkill += UpdateSkillUI;
            PassiveControllerCompo.OnTakePasiveSkill += UpdatePassiveSkillUI;

            if (newInteractionSkillUI != null)
            {
                newInteractionSkillUI.OnShow += HandleInteractionSkillUIShown;
                newInteractionSkillUI.OnHide += HandleInteractionUIHidden;
            }
            if (newInteractionPassiveUI != null)
            {
                newInteractionPassiveUI.OnShow += HandleInteractionPassiveUIShown;
                newInteractionPassiveUI.OnHide+= HandleInteractionPassiveUIHidden;
            }
            else Debug.LogError("UI is null");
        }

        private void HandleInteractionPassiveUIShown()
        {
            
        }
        
        private void HandleInteractionPassiveUIHidden()
        {
            
        }
        
        private void HandleInteractionSkillUIShown()
        {
            if (SkillUI == null || SkillUI.Length == 0)
            {
                Debug.LogWarning("[NewUIManager] SkillUI array is empty.");
                return;
            }

            var first = SkillUI[0];
            if (first == null)
            {
                Debug.LogWarning("[NewUIManager] SkillUI[0] is null.");
                return;
            }

            var sd = first.GetCurrentSkillData();
            if (sd != null && oldInteractionSkillUI != null)
            {
                oldInteractionSkillUI.Show(sd);
            }
            else
            {
                oldInteractionSkillUI?.Hide();
            }
        }

        private void HandleInteractionUIHidden()
        {
            oldInteractionSkillUI?.Hide();
        }
        
        // 현재 보유 중인 스킬 리스트를 받아 UI 슬롯에 반영
        private void UpdateSkillUI()
        {
            var elements = SkillControllerCompo.Skills.ToArray();

            for (int i = 0; i < SkillUI.Length; i++)
            {
                // 슬롯이 비어있거나 null이면 초기화, 있으면 SkillData 전달
                if (i < elements.Length && elements[i] != null)
                    SkillUI[i].SetSkill(elements[i]);
                else
                    SkillUI[i].SetSkill(null);
            }
        }
        
        // 위랑 똑같은 패시브 UI 갱신
        private void UpdatePassiveSkillUI(PassiveSkill newSkill)
        {
            for (int i = 0; i < PassiveSkillUI.Length; i++)
            {
                if (PassiveSkillUI[i].HasData == false)
                {
                    PassiveSkillUI[i].SetPassiveData(newSkill.Data);
                    return;
                }
            }
        }

        private void OnDisable()
        {
            SkillControllerCompo.OnChangeSkill -= UpdateSkillUI;
            PassiveControllerCompo.OnTakePasiveSkill -= UpdatePassiveSkillUI;
            
            newInteractionSkillUI.OnShow -= HandleInteractionSkillUIShown;
            newInteractionSkillUI.OnHide -= HandleInteractionUIHidden;
        }
    }
}