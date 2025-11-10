using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "SO/Enemy/BulletData", order = 0)]
public class BulletData : ScriptableObject
{
    public GameObject projectilePrefab; // 발사체
    public float lifeTime;
    public float bulletSpeed = 5;
    
    [Header("Parabola Settings")]
    public bool parabola = false;
    public float arcHeight = 2f; // 포물선 높이
}
