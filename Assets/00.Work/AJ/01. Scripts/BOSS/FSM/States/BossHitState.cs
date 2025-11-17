using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossHitState : BossState
    {
        private BossHit _bossHit;
        private Vector2 _knockbackDir;
        private float _timer;
        private float _knockbackDistance = 0.2f;
        private float _knockbackDuration = 0.25f;
        public BossHitState(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossHit = boss.GetComponent<BossHit>();
        }
        public override void Enter()
        {
            base.Enter();
            
            _knockbackDir = (_boss.transform.position - _boss.Target.position).normalized;
            
            KnockBack();
        }

        private void KnockBack()
        {
            Vector2 startPos = _boss.transform.position;
            Vector2 desiredTargetPos = startPos + _knockbackDir * _knockbackDistance;
            float bossRadius = 0.5f;
            if (_boss.ColliderCompo is CircleCollider2D circleCollider)
            {
                bossRadius = circleCollider.radius * _boss.transform.localScale.x;
            }
            RaycastHit2D hit = Physics2D.CircleCast(startPos, bossRadius, _knockbackDir, _knockbackDistance, _boss.whatIsWall);
            Vector2 safeTargetPos = desiredTargetPos;
            if (hit.collider != null)
            {
                float safeDistance = hit.distance - 0.1f;
                if(safeDistance < 0) safeDistance = 0;
                safeTargetPos = startPos + _knockbackDir * safeDistance;
            }

            _boss.RbCompo.linearVelocity = Vector2.zero;
            _boss.transform.DOMove(safeTargetPos, _knockbackDuration);
        }

        public override void Update()
        {
            base.Update();
            
            if (_bossHit.isAnimationEnd)
            {
                _boss.IsHit = false;
                _bossHit.isAnimationEnd = false;
                _stateMachine.ChangeState(BossStateType.Idle);
            }
        }
    }
}