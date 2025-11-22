using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class UltimateBox : AbstractBox
    {
        [SerializeField] private List<GameObject> items = new List<GameObject>();
        
        protected override void ItemScattering()
        {
            if (items == null || items.Count == 0)
            {
                Debug.LogWarning($"[{nameof(UltimateBox)}] 아이템 리스트가 비어 있습니다.");
                return;
            }

            GameObject selectedItem = items[Random.Range(0, items.Count)];

            StartCoroutine(Buffer(selectedItem));
        }

        protected override void OnDropCreated(GameObject drop)
        {
            if (drop == null)
            {
                Debug.LogWarning("Drop 생성 실패: null");
                return;
            }

            drop.transform.localScale = Vector3.zero;

            Vector2 randomDir = Random.insideUnitCircle.normalized;
            Vector3 targetPos = transform.position + (Vector3)(randomDir * ScatteringRange);

            Sequence seq = DOTween.Sequence();

            seq.Append(drop.transform.DOScale(Vector3.one, 0.3f))
                .Append(drop.transform.DOMove(targetPos, 0.4f))
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    SetDropInteractable(drop, true);
                });
        }
    }
}