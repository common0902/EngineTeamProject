using System.Collections.Generic;
using _00.Work.AJ._01._Scripts.BOSS;
using _00.Work.Yeonwoo._01.Scripts.Direction;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class BossDeathAfterCinema : MonoBehaviour
    {
        private Boss _boss;

        [Header("Fire sources (선택)")]
        [Tooltip("프리팹으로 생성할 Fire들 (프리팹을 넣으면 Instantiate 사용). 비워두면 scene에 있는 비활성 Fire들을 사용합니다.")]
        [SerializeField] private Fire[] firePrefabs;

        [Tooltip("씬 내부에 이미 배치되어 있는 Fire들을 사용하려면 여기 체크")]
        [SerializeField] private bool useExistingFires = false;

        [Tooltip("씬 내부의 Fire를 자동으로 찾아 사용(비활성 포함)")]
        [SerializeField] private Fire[] existingFires;

        [Header("Spawn / Sequence 설정")]
        [SerializeField] private Transform spawnParent; // 생성 시 부모로 쓸 Transform (없으면 this.transform)
        [SerializeField] private float spawnInterval = 0.25f;
        [SerializeField] private float entranceDuration = 0.25f;
        [SerializeField] private float winDuration = 0.6f;

        [Header("Scene Transition")]
        [Tooltip("빈값이면 씬 전환하지 않습니다.")]
        [SerializeField] private string nextSceneName = "";
        [SerializeField] private bool loadNextScene = true;
        [SerializeField] private float delayBeforeLoad = 0.5f;

        private void Awake()
        {
            _boss = FindAnyObjectByType<Boss>();

            // 기존 Fire들을 자동으로 찾을지, 인스펙터에 수동으로 넣을지 선택
            if (useExistingFires)
            {
                // includeInactive: true로 비활성 오브젝트도 찾음
                existingFires = GetComponentsInChildren<Fire>(true);
            }
        }

        private void OnEnable()
        {
            if (_boss != null && _boss.DeathState != null)
                _boss.DeathState.BossDeathAction += FinalCinema;
        }

        private void OnDisable()
        {
            if (_boss != null && _boss.DeathState != null)
                _boss.DeathState.BossDeathAction -= FinalCinema;
        }

        private void FinalCinema()
        {
            // 실행 경로 결정: 프리팹 사용 우선, 없으면 existingFires 사용
            if (firePrefabs != null && firePrefabs.Length > 0)
            {
                PlaySequenceWithPrefabs();
            }
            else if (existingFires != null && existingFires.Length > 0)
            {
                PlaySequenceWithExisting();
            }
            else
            {
                // 아무 것도 없으면 즉시 씬 로드(혹은 아무 동작 없음)
                if (loadNextScene && !string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
            }
        }

        private void PlaySequenceWithPrefabs()
        {
            Transform parent = spawnParent != null ? spawnParent : this.transform;
            Sequence seq = DOTween.Sequence();

            foreach (var prefab in firePrefabs)
            {
                // Instantiate but keep it inactive until Entrance play (or set active and animate)
                seq.AppendCallback(() =>
                {
                    Fire f = Instantiate(prefab, parent);
                    f.transform.localScale = Vector3.one;
                    // 초기화(비활성 상태에서 PlayEntrance 사용하면 내부에서 활성화함)
                });

                // 작은 지연 후에 생성된 오브젝트의 PlayEntrance/PlayWin을 붙임
                seq.AppendInterval(0.01f); // 콜백이 적용될 시간을 줌

                seq.AppendCallback(() =>
                {
                    // 최근에 생성된 child로 찾아서 실행
                    Fire f = parent.GetComponentInChildren<Fire>(true);
                    if (f != null)
                    {
                        // PlayEntrance + PlayWin을 순차로 append
                        seq.Append(f.PlayEntrance(entranceDuration));
                        seq.Append(f.PlayWin(winDuration));
                    }
                });

                // 기본 간격
                seq.AppendInterval(spawnInterval);
            }

            seq.AppendInterval(delayBeforeLoad);
            seq.OnComplete(() =>
            {
                if (loadNextScene && !string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
            });

            seq.SetUpdate(true);
        }

        private void PlaySequenceWithExisting()
        {
            Transform parent = spawnParent != null ? spawnParent : this.transform;
            Sequence seq = DOTween.Sequence();

            // existingFires를 사용. 순서대로 PlayEntrance -> PlayWin
            foreach (var f in existingFires)
            {
                seq.AppendCallback(() =>
                {
                    if (f == null) return;
                    f.gameObject.SetActive(true);
                });

                seq.Append(f.PlayEntrance(entranceDuration));
                seq.Append(f.PlayWin(winDuration));
                seq.AppendInterval(spawnInterval);
            }

            seq.AppendInterval(delayBeforeLoad);
            seq.OnComplete(() =>
            {
                if (loadNextScene && !string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
            });

            seq.SetUpdate(true);
        }
    }
}
