using System;
using System.Collections;
using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class BossLaser : MonoBehaviour
    {
        [SerializeField] private GameObject laser;
        private Boss _boss;
        private float _dmg;
        private bool _isDamaging;
        private Coroutine _damageCoroutine;
        [SerializeField] private float damageTickInterval = 0.3f; 
        [SerializeField]private float laserWidth = 1f;
        public Action OnLaserHitEnd;
        private void Awake()
        {
            laser.transform.localScale = new Vector3(0f, 1f, 1f);
        }

        [ContextMenu("Laser")]
        public void Init(Vector3 pos, Boss boss)
        {
            _boss = boss;
            _dmg = boss.Damage;
            CameraHandler.Instance.ShakeCamera(0.03f, 8f);
            transform.position = new Vector3(pos.x - 11f, pos.y, 0f);
            gameObject.GetComponentInChildren<LaserBoss>().Init(_boss, 1f, 0f, _boss.playerMask);
            Sequence seq = DOTween.Sequence();
            seq.Append(laser.transform.DOScaleX(22f, 2f)
                .SetDelay(5f)
                .OnComplete(() =>
                {
                    laser.transform.DOScaleX(0f, 1f).OnComplete(() => Destroy(gameObject));
                    OnLaserHitEnd?.Invoke();
                }));
            seq.JoinCallback(() => SoundManager.Instance.PlaySound("BossLaser2"));
        }

        private void OnDrawGizmos()
        {
            if (laser != null)
            {
                Gizmos.color = Color.red;
                float currentLength = laser.transform.localScale.x;
                Vector3 center = laser.transform.position + laser.transform.right * (currentLength / 2f);
                Gizmos.DrawWireCube(center, new Vector3(currentLength, laserWidth, 0f));
            }
        } 
    }
}
