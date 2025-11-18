using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates
{
    public class BossAttackBombState : BossState
    {
        private BossAttack _bossAttack;
        private bool _isMoving;
        public BossAttackBombState(Boss boss, string animName, BossStateMachine stateMachine) 
            : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossAttack>();
        }

        public override void Enter()    
        {
            base.Enter();
            Debug.Log("Bomb Attack");
            _boss.RbCompo.linearVelocity = Vector2.zero;
            _isMoving = true;
            MoveToCenter();
        }
        private void MoveToCenter()
        {
            _boss.transform.DOMove(_boss.CenterPos.position, 2f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    _isMoving = false;
                });
        }
        public override void Update()
        {
            if (_bossAttack.IsAnimationEnd) 
            {
                _bossAttack.IsAnimationEnd = false;
                _stateMachine.ChangeState(BossStateType.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}