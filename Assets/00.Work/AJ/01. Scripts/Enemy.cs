using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [field: SerializeField] public EnemySO enemySO { get; private set; }
    

    private void OnValidate()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = enemySO.enemySprite;

    }
}
