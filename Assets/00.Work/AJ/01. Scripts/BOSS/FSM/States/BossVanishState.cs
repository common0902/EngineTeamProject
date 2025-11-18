using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossVanishState : BossState
    {
        private BossAnimator _animator;
        private BossAttack _bossAttack;
        private float _timer;
        private float _vanishDuration = 2f;
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
            _boss.transform.DOMove(_boss.CenterPos.position + Vector3.up * 10, 2f);
        }

        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _vanishDuration)
            {
                _stateMachine.ChangeState(BossStateType.Appear);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _animator.OnVanishTrigger -= OnVanishComplete;
        }
    }
}