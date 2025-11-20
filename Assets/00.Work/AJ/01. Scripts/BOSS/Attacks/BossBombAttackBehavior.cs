using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Sequence = DG.Tweening.Sequence;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossBombAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss;
        private float _dotDuration = 20f;        
        private float _dotTickInterval = 0.5f;  
        private float _dotDamageMultiplier = 2f;
        private Sequence _sequence;
        public void Initialize(Boss boss)
        {
             _boss = boss;
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            _boss.Speed = 0f;
            yield return new WaitForSeconds(2f);
            CameraHandler.Instance.ShakeCamera(0.03f, 20f);
            _sequence = DOTween.Sequence();
            _sequence.Append(_boss.RangeSprite.DOFade(0.5f, 10f));
            _sequence.AppendCallback(() =>
            {
                var bomb = Object.Instantiate(_boss.Bomb, _boss.transform.position, Quaternion.identity);
                Debug.Log(bomb);
                var cic = bomb.GetComponent<Circle>();
                cic.Init(_boss, _dotDamageMultiplier, _dotTickInterval);
                bomb.transform.localScale = Vector3.zero;
                var attackSeq = DOTween.Sequence();
                attackSeq.Append(bomb.transform.DOScale(15f, 0.5f));
                attackSeq.AppendInterval(7);
                attackSeq.Append(bomb.GetComponent<SpriteRenderer>().DOFade(0f, 1f));
                attackSeq.AppendCallback(() =>
                {
                    Object.Destroy(bomb);
                    _boss.RangeSprite.DOFade(0f, 1f);
                    _boss.HealthCompo.Invincibility = false;
                });
            });
        }
        public void OnAttackAnimationEnd()
        {
            _sequence?.Kill();
            _sequence = null;
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}