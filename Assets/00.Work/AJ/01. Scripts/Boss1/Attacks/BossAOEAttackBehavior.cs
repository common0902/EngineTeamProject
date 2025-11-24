using System.Collections;
using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossAoeAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss;
        private SpriteRenderer _aoeRangeSprite;
        private Sequence _aoeSequence;
        private float _dotDuration = 100f;
        private float _dotTickInterval = 0.1f;
        private float _dotDamageMultiplier = 10f;
        public void Initialize(Boss boss)
        {
            _boss = boss;
            _dotTickInterval = 0.1f;
            _boss.HealthCompo.Invincibility = true;
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            CameraHandler.Instance.ShakeCamera(0.07f, 20f);
            SoundManager.Instance.PlaySound("Earthquake");
            yield return new WaitForSeconds(2f);
            var instance = Object.Instantiate(_boss.AoeSprite.gameObject,
                _boss.CenterPos.position + new Vector3(0f, 0.85f, 0f), Quaternion.identity);
            _aoeRangeSprite = instance.GetComponent<SpriteRenderer>();
            _aoeRangeSprite.color = new Color(1, 0, 0, 0);
            _aoeSequence = DOTween.Sequence();
            _aoeSequence.Append(_aoeRangeSprite.DOFade(0.5f, 5f));
            _aoeSequence.JoinCallback(() => SoundManager.Instance.PlaySound("LaserPrepare"));
            _aoeSequence.AppendCallback(() =>
            {
                var aoe = Object.Instantiate(_boss.Aoe, _boss.CenterPos.position + new Vector3(0f, 30f, 0f),
                    _boss.Aoe.transform.rotation);
                SoundManager.Instance.PlaySound("BossLaser");
                var aoeBox = aoe.GetComponent<AoeAttack>();
                if (aoeBox != null)
                {
                    Debug.Log(_dotTickInterval);
                    aoeBox.Initialize(_boss, _dotDamageMultiplier, _dotTickInterval, _boss.playerMask);
                }
                
                var moveSequence = DOTween.Sequence();
                moveSequence.Append(aoe.transform.DOMove(_boss.CenterPos.position + new Vector3(0f, 12.2f, 0f), 5f));
                moveSequence.AppendInterval(4.5f);
                moveSequence.Append(aoe.transform.DOMove(_boss.CenterPos.position + new Vector3(0f, 100f, 0f), 1f));
                moveSequence.JoinCallback(() => SoundManager.Instance.PlaySound("LaserEnd"));
                moveSequence.AppendCallback(() =>
                {
                    _aoeRangeSprite.DOFade(0f, 1f).OnComplete(() =>
                    {
                        if (_aoeRangeSprite != null)
                        {
                            Object.Destroy(_aoeRangeSprite.gameObject);
                            _aoeRangeSprite = null;
                        }
                
                        if (aoe != null)
                        {
                            Object.Destroy(aoe.gameObject);
                        }

                        _boss.HealthCompo.Invincibility = false;
                        OnAttackAnimationEnd();
                    });
                });
            });
        }
        private void CleanupSequence()
        {
            _aoeSequence?.Kill();
            _aoeSequence = null;
        }

        public void OnAttackAnimationEnd()
        {
            CleanupSequence();
            IsAttackAnimationEnd = true;
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}