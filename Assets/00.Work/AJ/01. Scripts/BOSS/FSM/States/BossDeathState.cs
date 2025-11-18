using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace _00.Work.AJ._01._Scripts.BOSS.FSM.States
{
    public class BossDeathState : BossState
    {
        private BossHit _bossHit;
        private float _timer;
        private float _waitTime = 5f;
        private Image _whiteImg;
        private RectTransform _whiteRect;
        private Sequence _sequence;
        public BossDeathState(Boss boss, string animName, BossStateMachine stateMachine) : base(boss, animName, stateMachine)
        {
            _bossHit = boss.GetComponent<BossHit>();
            _whiteImg = _boss.white.GetComponent<Image>();
            _whiteRect = _boss.white.GetComponent<RectTransform>();
            _sequence = DOTween.Sequence();
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Death");
            CameraHandler.Instance.ShakeCamera(0.03f, 11f);
            Player.Instance.PlayerMoveCompo._isCasting = true;
            _whiteRect.localScale = new Vector3(0f, 0f, 1f);
            _whiteImg.color = new Color(1f,1f,1f,0.7f);
            _whiteRect.transform.position = Camera.main.WorldToScreenPoint(_boss.transform.position);
            _sequence.Append(_whiteRect.DOScaleY(3f, 0.5f));
            _sequence.AppendInterval(1f);    
            _sequence.Append(_whiteRect.DOScaleX(3f, 1f));
            
            _timer = 0f;
        }

        public override void Update()
        {
            _boss.RbCompo.linearVelocity = Vector2.zero;
            if (_bossHit.isAnimationEnd)
            {
                _timer += Time.deltaTime;
                if (_timer >= _waitTime)
                {
                    _whiteImg.DOFade(0f, 1f).OnComplete(() =>
                    {
                        Player.Instance.PlayerMoveCompo._isCasting = false;
                        _whiteRect.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, 0f));
                        _whiteRect.localScale = new Vector3(1f, 1f, 1f);
                        Object.Destroy(_boss.gameObject, 3f);
                    });
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}