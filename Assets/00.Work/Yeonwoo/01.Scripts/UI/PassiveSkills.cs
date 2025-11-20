using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PassiveSkills : AbstractSkills
    {
        [SerializeField] private PassiveData passiveData;
        
        protected override void OnEnable()
        { 
            base.OnEnable();
        }
    }
}