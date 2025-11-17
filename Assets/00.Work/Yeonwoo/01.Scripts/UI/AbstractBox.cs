using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public abstract class AbstractBox : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> items = new List<GameObject>();
        
        public Sprite openBoxSprite;
        private SpriteRenderer _spriteRenderer;
        private InteractionBox _boxOpenAction;
        private BoxCollider2D _collider;
        protected readonly float ScatteringRange = 1.2f;
        
        protected virtual void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _boxOpenAction = GetComponent<InteractionBox>();
            _boxOpenAction.OnBoxOpen += BoxOpenAnim;
        }

        protected virtual void BoxOpenAnim()
        {
            _spriteRenderer.sprite = openBoxSprite;
            _collider.isTrigger = true;
            _boxOpenAction.InteractionText.enabled = false;
            ItemScattering();
        }

        protected virtual void OnDestroy()
        {
            _boxOpenAction.OnBoxOpen -= BoxOpenAnim;
        }

        protected virtual void ItemScattering() // 얘 나중에 자식으로 옮기기
        {
            if (items == null || items.Count == 0)
            {
                Debug.LogWarning("아이템 리스트가 비어 있습니다."); 
                return;
            }
            
            GameObject selectedItem = items[Random.Range(0, items.Count)];
            GameObject drop = Instantiate(selectedItem, transform.position, Quaternion.identity);
            
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