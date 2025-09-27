using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    public Sprite enemySprite;
    public float Health;
    public float Speed;
    public float Damage;
    public EnemyType enemyType;
}
