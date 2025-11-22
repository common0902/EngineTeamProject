using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class GoldBox : AbstractBox
    {
        [SerializeField] private List<GameObject> items = new List<GameObject>();

        protected override void Awake()
        {
            base.Awake();
            SharedItemPool.AddItems(items, nameof(GoldBox));
        }

        protected override void ItemScattering()
        {
            GameObject selectedItem = SharedItemPool.PopNext(nameof(GoldBox));

            if (selectedItem == null)
            {
                Debug.LogWarning("[GoldBox] 씬 전체(GoldBox 풀)에서 사용할 아이템이 없습니다.");
                return;
            }

            StartCoroutine(Buffer(selectedItem));
        }

        protected override void OnDropCreated(GameObject drop)
        {
            if (drop == null)
            {
                Debug.LogWarning("[GoldBox] Drop 생성 실패: null");
                return;
            }

            drop.transform.localScale = Vector3.zero;

            Vector2 randomDir = Random.insideUnitCircle.normalized;
            Vector3 targetPos = transform.position + (Vector3)(randomDir * ScatteringRange);

            Sequence seq = DOTween.Sequence();

            seq.Append(drop.transform.DOScale(Vector3.one, 0.3f))
                .Append(drop.transform.DOMove(targetPos, 0.4f))
                .SetEase(Ease.OutQuad)
                .OnComplete(() => SetDropInteractable(drop, true));
        }
    }
}