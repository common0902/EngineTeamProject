using System;
using _00.Work.SYH._02Script.ETG;
using Unity.Cinemachine;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2
{
    public class Bullet : MonoBehaviour
    {
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private Rigidbody2D _rb;
        private Animator _anim;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _speed = 10f;
        private MiddleBoss _boss;
        private BossClones.BossClone _cloneboss;
        private float _damageMultiplier = 1f;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        
        
        public void SetUp(MiddleBoss boss, Vector2 dir, float speed = 1f, float damageMultiplier = 1f)
        {
            _boss = boss;
            _damageMultiplier = damageMultiplier;
            _speed = speed;
            dir = dir.normalized;
            _rb.linearVelocity = dir * _speed;
            Vector2 blendDir = dir;
            _spriteRenderer.flipX = blendDir.x > 0;
            _anim.SetFloat(MoveX, blendDir.x);
            _anim.SetFloat(MoveY, blendDir.y);
            Destroy(gameObject, 3f);
        }
        public void SetUp(BossClones.BossClone boss, Vector2 dir, float speed = 1f, float damageMultiplier = 1f)
        {
            _cloneboss = boss;
            _damageMultiplier = damageMultiplier;
            _speed = speed;
            dir = dir.normalized;
            _rb.linearVelocity = dir * _speed;
            Vector2 blendDir = dir;
            _spriteRenderer.flipX = blendDir.x > 0;
            _anim.SetFloat(MoveX, blendDir.x);
            _anim.SetFloat(MoveY, blendDir.y);
            Destroy(gameObject, 3f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.PlayerHealthSystemCompo.Damage(_boss.Damage * _damageMultiplier);
            }
            Destroy(gameObject);
        }
    }
}
