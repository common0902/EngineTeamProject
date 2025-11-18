using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public abstract class AbstractBox : MonoBehaviour
    {
        public Sprite openBoxSprite;
        private SpriteRenderer _spriteRenderer;
        private InteractionBox _boxOpenAction;
        private BoxCollider2D _collider;
        protected readonly float ScatteringRange = 1.2f;
        private GameObject _drop;
        
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

        protected virtual IEnumerator Buffer(GameObject item)
        {
            _drop = Instantiate(item, transform.position, Quaternion.identity);
            yield return null;
            OnDropCreated(_drop);
        }

        protected abstract void ItemScattering();
        
        protected abstract void OnDropCreated(GameObject drop);
    }
}