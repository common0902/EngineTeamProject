using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossLaserAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss;
        private Sequence _sequence;
        private List<Transform> _laserList = new List<Transform>();
        public void Initialize(Boss boss)
        {
            _boss = boss;
            _sequence = DOTween.Sequence();
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            Debug.Log("Laser");
            int rand = 3;
            for (int i = 0; i < rand; i++)
            {
                GameObject obj = Object.Instantiate(_boss.laserPoint, _boss.transform.position, Quaternion.identity);
                _laserList.Add(obj.transform);
                Debug.Log("LaserRangeSpawned");
            }

            for(int i = 0; i < rand; i++)
            {
                float yOffset = (1 - i) * 5f;
                _laserList[i].transform.position = _boss.CenterPos.position + new Vector3(0, yOffset, 0);
                Debug.Log("LaserRangePositionSet");
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < rand; i++)
            {
                var laser = Object.Instantiate(_boss.laserPrefab, 
                    _laserList[i].transform.position,
                    Quaternion.identity).GetComponentInChildren<BossLaser>();
                
                Debug.Log("LaserFire");
                laser.Init(_laserList[i].transform.position, _boss);
                yield return new WaitForSeconds(0.5f);
            }

            foreach (var laser in _laserList)
            {
                Object.Destroy(laser.gameObject);
            }
            OnAttackAnimationEnd();
        }

        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
            _laserList.Clear();
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}