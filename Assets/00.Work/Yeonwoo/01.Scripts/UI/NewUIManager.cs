using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class NewUIManager : MonoBehaviour
    {
        [field: SerializeField] public SkillUI[] SkillUI { get; private set; }
        [field: SerializeField] public UltimateSkillUI[] UltimateSkillUI { get; private set; }
        [field: SerializeField] public PassiveSkillUI[] PassiveSkillUI { get; private set; }

        [field: SerializeField] public SkillController SkillControllerCompo { get; private set; }
        [field: SerializeField] public PassiveSkillController PassiveControllerCompo { get; private set; }

        [SerializeField] private NewInteractionSkillUI newInteractionSkillUI;
        [SerializeField] private OldInteractionSkillUI oldInteractionSkillUI;
        [SerializeField] private NewInteractionPassiveUI newInteractionPassiveUI;

        private void OnEnable()
        {
            if (SkillControllerCompo != null)
            {
                SkillControllerCompo.OnChangeSkill += UpdateSkillUI;
                SkillControllerCompo.OnChangeUltimateSkill += UpdateUltimateSkillUI;
            }

            if (newInteractionSkillUI != null)
            {
                newInteractionSkillUI.OnShow += HandleInteractionSkillUIShown;
                newInteractionSkillUI.OnHide += HandleInteractionUIHidden;
            }

            if (newInteractionPassiveUI != null)
            {
                newInteractionPassiveUI.OnShow += HandleInteractionPassiveUIShown;
                newInteractionPassiveUI.OnHide += HandleInteractionPassiveUIHidden;
            }
            
            SafeUpdateAllUI();
        }

        private void OnDisable()
        {
            if (SkillControllerCompo != null)
            {
                SkillControllerCompo.OnChangeSkill -= UpdateSkillUI;
                SkillControllerCompo.OnChangeUltimateSkill -= UpdateUltimateSkillUI;
            }

            if (newInteractionSkillUI != null)
            {
                newInteractionSkillUI.OnShow -= HandleInteractionSkillUIShown;
                newInteractionSkillUI.OnHide -= HandleInteractionUIHidden;
            }

            if (newInteractionPassiveUI != null)
            {
                newInteractionPassiveUI.OnShow -= HandleInteractionPassiveUIShown;
                newInteractionPassiveUI.OnHide -= HandleInteractionPassiveUIHidden;
            }
        }

        private void SafeUpdateAllUI()
        {
            UpdateUltimateSkillUI();
            UpdateSkillUI();
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
                Debug.LogWarning("[NewUIManager] SkillUI array is empty or null.");
                oldInteractionSkillUI?.Hide();
                return;
            }

            var first = SkillUI[0];
            if (first == null)
            {
                Debug.LogWarning("[NewUIManager] SkillUI[0] is null.");
                oldInteractionSkillUI?.Hide();
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
            if (SkillControllerCompo == null)
            {
                Debug.LogWarning("[NewUIManager] SkillControllerCompo is null. Cannot UpdateSkillUI.");
                if (SkillUI != null)
                {
                    for (int i = 0; i < SkillUI.Length; i++) SkillUI[i]?.SetSkill(null);
                }
                return;
            }

            var skills = SkillControllerCompo.Skills;
            if (skills == null)
            {
                Debug.LogWarning("[NewUIManager] SkillControllerCompo.Skills is null.");
                if (SkillUI != null)
                {
                    for (int i = 0; i < SkillUI.Length; i++) SkillUI[i]?.SetSkill(null);
                }
                return;
            }

            for (int i = 0; i < SkillUI.Length; i++)
            {
                if (i < skills.Count && skills[i] != null)
                    SkillUI[i]?.SetSkill(skills[i]);
                else
                    SkillUI[i]?.SetSkill(null);
            }
        }

        private void UpdateUltimateSkillUI()
        {
            if (UltimateSkillUI == null || UltimateSkillUI.Length == 0)
            {
                Debug.LogWarning("[NewUIManager] UltimateSkillUI array is empty or null.");
                return;
            }

            if (SkillControllerCompo == null)
            {
                Debug.LogWarning("[NewUIManager] SkillControllerCompo is null. Cannot UpdateUltimateSkillUI.");
                for (int i = 0; i < UltimateSkillUI.Length; i++) UltimateSkillUI[i]?.SetSkill(null);
                return;
            }

            var ultimate = SkillControllerCompo.UltimateSkill;
            
            var first = UltimateSkillUI[0];
            if (first == null)
            {
                Debug.LogWarning("[NewUIManager] UltimateSkillUI[0] is null.");
                return;
            }

            first.SetSkill(ultimate != null ? ultimate : null);
            
            for (int i = 1; i < UltimateSkillUI.Length; i++)
            {
                UltimateSkillUI[i]?.SetSkill(null);
            }
        }
    }
}
