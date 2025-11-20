using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates
{
    public class BossAttackBombState : BossState
    {
        private BossAttack _bossAttack;
        private bool _isMoving;
        private float _timer;
        private float _waitTime = 21f;
        public BossAttackBombState(Boss boss, string animName, BossStateMachine stateMachine) 
            : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossAttack>();
        }

        public override void Enter()    
        {
            base.Enter();
            Debug.Log("Bomb Attack");
            _isMoving = true;
            MoveToCenter();
            _boss.HealthCompo.Invincibility = true;
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
            if (_isMoving)
                return;
            _boss.RbCompo.linearVelocity = Vector2.zero;
            _timer += Time.deltaTime;
            if (_bossAttack.IsAnimationEnd && _timer > _waitTime) 
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