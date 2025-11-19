using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossBombAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss;
        private float _dotDuration = 5f;        
        private float _dotTickInterval = 0.5f;  
        private float _dotDamageMultiplier = 2f; 
        public void Initialize(Boss boss)
        {
             _boss = boss;
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            _boss.Speed = 0f;
            yield return new WaitForSeconds(2f);
            CameraHandler.Instance.ShakeCamera(0.03f, 20f);
            _boss.RangeSprite.DOFade(0.5f, 10f).OnComplete(() =>
            {
                var bomb = Object.Instantiate(_boss.Bomb, _boss.transform.position, Quaternion.identity,
                    _boss.transform);
                if (bomb.GetComponent<Circle>().GetCollision())
                {
                    _boss.StartCoroutine(ApplyDotDamage(bomb));
                }

                bomb.transform.localScale = new Vector3(0f, 0f, 0f);
                bomb.transform.DOScale(10f, 0.5f);
            })
            .OnComplete(OnAttackAnimationEnd);
        }
        private IEnumerator ApplyDotDamage(GameObject bomb)
        {
            float elapsedTime = 0f;
            Circle bombCircle = bomb.GetComponent<Circle>();
            
            if (bombCircle.GetCollision())
            {
                Player.Instance.PlayerHealthSystemCompo.Damage(_boss.Damage * 10);
            }
            
            while (elapsedTime < _dotDuration)
            {
                yield return new WaitForSeconds(_dotTickInterval);
                elapsedTime += _dotTickInterval;
                
                if (bombCircle.GetCollision())
                {
                    Player.Instance.PlayerHealthSystemCompo.Damage(_boss.Damage * _dotDamageMultiplier);
                }
            }

            bomb.GetComponent<SpriteRenderer>().DOFade(0f, 1f).OnComplete(() =>
            {
                Debug.Log("아앙");
                Object.Destroy(bomb);
                _boss.RangeSprite.DOFade(0f, 1f).OnComplete(OnAttackAnimationEnd);
            });
            
        }
        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}