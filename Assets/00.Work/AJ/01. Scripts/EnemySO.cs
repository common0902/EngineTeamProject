using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    public Sprite enemySprite;
    public float health;
    public float speed;
    public float damage;
    public EnemyType enemyType;
}
