using System.Runtime.InteropServices.WindowsRuntime;
using _00.Work.SYH._02Script.ETG;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MBossDashState : MiddleBossState
    {
        private float _dashDistance = 50f;
        private float _dashDuration = 10f;
        private float _spawnDelay = 5f;
        private bool _dashCompleted = false;
        private HealthSystem _targetHealth;

        public MBossDashState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _targetHealth = boss.Target.GetComponent<HealthSystem>();
        }
        public override void Enter()
        {
            base.Enter();
            _boss.CanFilp(false);
            _boss.VisualCompo.Flip(Vector2.left);
            _dashCompleted = false;
            CameraHandler.Instance.ShakeCamera(0.07f, 10f);
            _boss.transform.DOMove(_boss.CenterPos.position + new Vector3(-_dashDistance, _boss.transform.position.y, 0f), _dashDuration)
                .OnComplete(() =>
                {
                    _boss.transform.position = _boss.CenterPos.position + new Vector3(_dashDistance, 0f, 0f);
                    SpawnBosses();
                });
        }

        private void SpawnBosses()
        {
            var bosses = _boss.Bosses;
            if (bosses == null || bosses.Count == 0)
                return;
            Sequence spawnSequence = DOTween.Sequence();
            
            for (int i = 0; i < bosses.Count; i++)
            {
                int index = i;
                Debug.Log($"Spawn {bosses[index].name}");
                spawnSequence.AppendCallback(() =>
                {
                    GameObject clone = Object.Instantiate(
                        bosses[index], 
                        _boss.transform.position, 
                        Quaternion.identity
                    );
                    
                    Vector3 targetPos = _boss.WayPoints.GetNextWayPoint();

                    clone.transform.DOMove(targetPos, 2f);
                });
                
                spawnSequence.AppendInterval(_spawnDelay);
            }
            
            spawnSequence.AppendCallback(() =>
            {
                Vector3 bossTarget = _boss.WayPoints.GetRandomWayPoint();
                _boss.transform.DOMove(bossTarget, 0.8f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        _dashCompleted = true;
                        _boss.CanFilp(true);
                    });
            });
        }

        public override void Update()
        {
            base.Update();
            if (_boss.CheckAttackRange())
            {
                _targetHealth.Damage(_boss.Damage * 3f);
            }
            if (_dashCompleted)
            {
                _boss.HealthCompo.Invincibility = false;
                _stateMachine.ChangeState(MiddleBossStateType.Idle);
            }
        }
        public override void Exit()
        {
            base.Exit();
            _dashCompleted = false;
            _boss.CanFilp(true);
            DOTween.Kill(_boss.transform);
        }
    }
}