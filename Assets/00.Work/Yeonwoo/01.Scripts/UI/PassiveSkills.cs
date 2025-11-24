using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PassiveSkills : AbstractSkills
    {
        [SerializeField] private PassiveData passiveData;
        private NewInteractionPassiveUI _newInteractionPassiveUI;

        private void Start()
        {
            passiveData = GetComponent<PassiveSkill>().Data;
        }
        protected override void OnEnable()
        { 
            base.OnEnable();
            _newInteractionPassiveUI = FindAnyObjectByType<NewInteractionPassiveUI>(FindObjectsInactive.Include); // 시간이... 없다..
            if (_newInteractionPassiveUI == null) 
                Debug.Log("No Interaction New Skill UI Found");
        }

        protected override void OnPlayerEnterRange()
        {
            if (_newInteractionPassiveUI == null || passiveData == null) return;
            
            if (!_newInteractionPassiveUI.IsShowing(passiveData))
                _newInteractionPassiveUI.Show(GetComponent<PassiveSkill>());
        }

        protected override void OnPlayerExitRange()
        {
            if (_newInteractionPassiveUI == null || passiveData == null) return;
            
            if (_newInteractionPassiveUI.IsShowing(passiveData))
                _newInteractionPassiveUI.Hide();
        }
    }
}