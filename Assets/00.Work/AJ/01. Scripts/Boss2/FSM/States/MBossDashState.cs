using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.FSM.States
{
    public class MBossDashState : MiddleBossState
    {
        private float _dashDistance = 50f;
        private float _dashDuration = 2f;
        private float _spawnDelay = 0.3f;
        private bool _dashCompleted = false;

        public MBossDashState(MiddleBoss boss, string animName, MiddleBossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            
        }
        public override void Enter()
        {
            base.Enter();
            _boss.CanFilp(false);
            _boss.VisualCompo.Flip(Vector2.left);
            _dashCompleted = false;
            _boss.transform.DOMove(_boss.CenterPos.position + new Vector3(-_dashDistance, _boss.transform.position.y, 0f), _dashDuration)
                .OnComplete(() =>
                {
                    _boss.transform.position = _boss.CenterPos.position + new Vector3(50f, 0f, 0f);
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
            if (_dashCompleted)
            {
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