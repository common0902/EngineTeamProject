using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private EnemySO enemySO;

    private void OnValidate()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = enemySO.enemySprite; 
    }
}
