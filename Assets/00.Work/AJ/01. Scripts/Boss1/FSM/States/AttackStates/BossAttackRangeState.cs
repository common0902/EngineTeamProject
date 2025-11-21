using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates
{
    public class BossAttackRangeState : BossState
    {
        private BossAttack _bossAttack;
        private bool _isMoving;
        private bool _hasStartedAttack;
        public BossAttackRangeState(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossAttack>();
        }
        public override void Enter()
        {
            base.Enter();
           // Debug.Log("Range Attack");
            _boss.RbCompo.linearVelocity = Vector2.zero;
            _isMoving = true;
            _hasStartedAttack = false;
            
            MoveToCorner();
        }

        private void MoveToCorner()
        {
            Vector2 targetPosition = _boss.WayPoints.GetRandomWayPoint();
            _boss.transform.DOMove(targetPosition, 2f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    _isMoving = false;
                });
        }

        public override void Update()
        {
            base.Update();
            
            if (_isMoving)
                return;
            
            if (!_hasStartedAttack)
            {
                _hasStartedAttack = true;
            }
            
            if (_bossAttack.IsAnimationEnd)
            {
                _bossAttack.IsAnimationEnd = false;
                _stateMachine.ChangeState(BossStateType.Idle);
            }
        }
        public override void Exit()
        {
            base.Exit();
            _boss.transform.DOKill();
        }
    }
}