using System;
using System.Collections;
using System.Collections.Generic;
using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public class BossLaserAttackBehavior : IBossAttackBehavior
    {
        private Boss _boss;
        private Sequence _sequence;
        private List<Transform> _laserList = new List<Transform>();
        private List<GameObject> _laserPointInstances = new List<GameObject>();
        private float _waitTime = 1f;
        public void Initialize(Boss boss)
        {
            _boss = boss;
            _sequence = DOTween.Sequence();
        }

        public IEnumerator ExecuteAttack(Vector2 direction)
        {
            Debug.Log("Laser");
            int rand = Random.Range(3, 6);
            float totalHeight = 10f;
            float spacing = (rand > 1) ? totalHeight / (rand - 1) : 0f;
            float startOffset = -totalHeight / 2f;
            for (int i = 0; i < rand; i++)
            {
                GameObject obj = Object.Instantiate(_boss.laserPoint, _boss.transform.position, Quaternion.identity);
                _laserList.Add(obj.transform);
                _laserPointInstances.Add(obj);
                Debug.Log("LaserRangeSpawned");
            }
            
            List<int> randomOrder = new List<int>();
            for (int i = 0; i < rand; i++)
            {
                randomOrder.Add(i);
            }
            for (int i = randomOrder.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (randomOrder[i], randomOrder[j]) = (randomOrder[j], randomOrder[i]);
            }
            for (int i = 0; i < rand; i++)
            {
                int idx = randomOrder[i];
                float yOffset = startOffset + spacing * idx;
                _laserList[idx].transform.position = _boss.CenterPos.position + new Vector3(0, yOffset, 0);
                Debug.Log("LaserRangePositionSet");
                yield return new WaitForSeconds(.5f);
            }

            yield return new WaitForSeconds(_waitTime);
            for (int i = randomOrder.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (randomOrder[i], randomOrder[j]) = (randomOrder[j], randomOrder[i]);
            }
            for (int i = 0; i < rand; i++)
            {
                var laser = Object.Instantiate(_boss.laserPrefab, 
                    _laserList[i].transform.position,
                    Quaternion.identity).GetComponent<BossLaser>();
                
                Debug.Log("LaserFire");
                int currentIndex = i;
                laser.Init(_laserList[i].transform.position, _boss);
                yield return new WaitForSeconds(0.5f);
                laser.OnLaserHitEnd += () =>
                {
                    if (currentIndex < _laserPointInstances.Count && _laserPointInstances[currentIndex] != null)
                    {
                        Object.Destroy(_laserPointInstances[currentIndex]);
                    }

                    _boss.transform.DOScale(Vector3.one * 1.5f, 0.5f);
                    _boss.HealthCompo.Invincibility = false;
                };
            }
        }

        public void OnAttackAnimationEnd()
        {
            IsAttackAnimationEnd = true;
            foreach (var laserPoint in _laserPointInstances)
            {
                if (laserPoint != null)
                {
                    Object.Destroy(laserPoint);
                }
            }
            
            _laserList.Clear();
            _laserPointInstances.Clear();
            _sequence?.Kill();
        }

        public bool IsAttackAnimationEnd { get; set; }
    }
}