using Unity.Behavior;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Bosses
{
    public class C1Boss : Agents.Agent
    {
        public BehaviorGraphAgent BtAgent { get; private set; }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            BtAgent = GetComponent<BehaviorGraphAgent>();
        }
    }
}