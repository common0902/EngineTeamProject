using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States.AttackStates
{
    public class BossAttackAOEState : BossState
    {
        private BossAttack _bossAttack;
        private float _aoeDuration = 22f; // 광역기 지속 시간
        private float _timer;
        
        public BossAttackAOEState(Boss boss, string animName, BossStateMachine stateMachine) 
            : base(boss, animName, stateMachine)
        {
            _bossAttack = boss.GetComponent<BossAttack>();
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("AOE Attack");
            _boss.RbCompo.linearVelocity = Vector2.zero;
            _timer = 0f;

            _boss.transform.DOMove(_boss.CenterPos.position, 1f);
        }

        public override void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _aoeDuration || _bossAttack.IsAnimationEnd)
            {
                _timer = 0f;
                _stateMachine.ChangeState(BossStateType.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}