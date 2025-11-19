using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossAoeAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss;
        private SpriteRenderer _aoeRangeSprite;
        private Sequence _aoeSequence;
        public void Initialize(Boss boss)
        {
            _boss = boss;
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            CleanupSequence();

            CameraHandler.Instance.ShakeCamera(0.07f, 25f);
            yield return new WaitForSeconds(2f);
            var instance = Object.Instantiate(_boss.AoeSprite.gameObject,
                _boss.CenterPos.position + new Vector3(0f, 0.85f, 0f), Quaternion.identity);
            _aoeRangeSprite = instance.GetComponent<SpriteRenderer>();
            _aoeRangeSprite.color = new Color(1, 0, 0, 0);
            _aoeSequence = DOTween.Sequence();
            _aoeSequence.Append(_aoeRangeSprite.DOFade(0.5f, 5f));
            _aoeSequence.AppendCallback(() =>
            {
                var aoe = Object.Instantiate(_boss.Aoe, _boss.transform.position + new Vector3(0f, 30f, 0f),
                    _boss.Aoe.transform.rotation);
                var aoeZone = aoe.GetComponentInChildren<AoeAttack>();
                if (aoeZone != null)
                {
                    float damagePerTick = _boss.Damage;
                    float tickInterval = 0.1f;
                    float duration = 500f;

                    aoeZone.Initialize(_boss, damagePerTick, tickInterval, duration, _boss.playerMask);
                }
                
                var moveSequence = DOTween.Sequence();
                moveSequence.Append(aoe.transform.DOMove(_boss.CenterPos.position + new Vector3(0f, 12.2f, 0f), 5f));
                moveSequence.AppendInterval(2);
                moveSequence.Append(aoe.transform.DOMove(_boss.CenterPos.position + new Vector3(0f, 40f, 0f), 1f));
            });
        }

        private void CleanupSequence()
        {
            _aoeSequence?.Kill();
            _aoeSequence = null;
        }

        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}