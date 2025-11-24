using UnityEngine;
using _00.Work.AJ._01._Scripts.BOSS.FSM.States;
using csiimnida.CSILib.SoundManager.RunTime;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MiddleBossDeathState : MiddleBossState
    {
        private MiddleBossHit _bossHit;
        public MiddleBossDeathState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossHit = boss.GetComponent<MiddleBossHit>();
        }
        public override void Enter()
        {
            base.Enter();
            SoundManager.Instance.PlaySound("Earthquake10s");
            CameraHandler.Instance.ShakeCamera(0.02f, 10f);
        }
        public override void Update()
        {
            _boss.RbCompo.linearVelocity = Vector2.zero;
            if (_bossHit.isAnimationEnd)
            {
                Object.Destroy(_boss.gameObject);
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}