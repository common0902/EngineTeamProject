using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossVanishState : BossState
    {
        private BossAnimator _animator;
        private BossAttack _bossAttack;
        
        public BossVanishState(Boss boss, string animName, BossStateMachine stateMachine) 
            : base(boss, animName, stateMachine)
        {
            _animator = boss.GetComponentInChildren<BossAnimator>();
            _bossAttack = boss.GetComponent<BossAttack>();
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Vanish");
            _boss.RbCompo.linearVelocity = Vector2.zero;
            
            _animator.OnVanishTrigger += OnVanishComplete;
        }

        private void OnVanishComplete()
        {
            MoveToSpecialPosition();
        }

        private void MoveToSpecialPosition()
        {
            _boss.transform.DOMove((Vector2)_boss.transform.position + Vector2.up * 30f, 3f)
                .OnComplete(() => _stateMachine.ChangeState(BossStateType.Appear));
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            _animator.OnVanishTrigger -= OnVanishComplete;
        }
    }
}