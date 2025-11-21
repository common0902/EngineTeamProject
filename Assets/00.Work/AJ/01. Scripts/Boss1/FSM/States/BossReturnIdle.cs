namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossReturnIdle : BossState
    {
        private BossAttack _attack;
        public BossReturnIdle(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _attack = boss.GetComponent<BossAttack>();
        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            if (_attack.IsAnimationEnd)
            {
                _stateMachine.ChangeState(BossStateType.Idle);
                return;
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}