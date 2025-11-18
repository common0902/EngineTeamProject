using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class DefaultBox : AbstractBox
    {
        [SerializeField] private List<GameObject> items = new List<GameObject>();
        
        protected override void ItemScattering()
        {
            if (items == null || items.Count == 0)
            {
                Debug.LogWarning("아이템 리스트가 비어 있습니다."); 
                return;
            }
            
            GameObject selectedItem = items[Random.Range(0, items.Count)];
            StartCoroutine(Buffer(selectedItem));
            GameObject drop = Drop;
            
            drop.transform.localScale = Vector3.zero;
            
            Vector2 randomDIr = Random.insideUnitCircle.normalized;

            Vector3 targetPos = transform.position + (Vector3)(randomDIr * ScatteringRange);
            
            Sequence seq = DOTween.Sequence();

            seq.Append(drop.transform.DOScale(Vector3.one, 0.5f))
                .Append(drop.transform.DOMove(targetPos, 0.7f))
                .SetEase(Ease.OutQuad);
        }
    }
}