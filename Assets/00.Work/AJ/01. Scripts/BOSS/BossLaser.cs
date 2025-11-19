using System.Collections;
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
        private float laserWidth = 0.1f;

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
            laser.transform.DOScaleX(11.88f, 5f).OnComplete(() =>
            {
                StopDamage();
                laser.transform.DOScaleX(0f, 1f);
            });
        }

        private void StartDamage()
        {
            if (!_isDamaging && _damageCoroutine == null)
            {
                _isDamaging = true;
                _damageCoroutine = StartCoroutine(DamageOverTime());
            }
        }
        private void StopDamage()
        {
            _isDamaging = false;
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
        }
        private IEnumerator DamageOverTime()
        {
            while (_isDamaging)
            {
                float currentLength = laser.transform.localScale.x;
                Vector2 origin = laser.transform.position;
                Vector2 direction = laser.transform.right;
                Vector2 size = new Vector2(currentLength, laserWidth);
                RaycastHit2D hit = Physics2D.BoxCast(origin,size,0f,direction,0f,_boss.playerMask);
            }

            yield return null;
        }
        
        private void OnDrawGizmos()
        {
            if (laser != null)
            {
                Gizmos.color = Color.red;
                float currentLength = laser.transform.localScale.x;
                Vector3 center = laser.transform.position + laser.transform.right * (currentLength / 2f);
                Gizmos.DrawWireCube(center, new Vector3(currentLength, laserWidth, 0.1f));
            }
        }
    }
}
