using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "SO/Enemy/BulletData", order = 0)]
public class BulletData : ScriptableObject
{
    public GameObject projectilePrefab; // 발사체
    public float lifeTime;
    public float bulletSpeed = 5;
    public bool multibulletShoot = false;
    [Range(0, 100)] public int bulletCount = 3;
    [Range(0, 360)] public float spreadAngle = 0f;
    
    public int GetBulletCount()
    {
        if(multibulletShoot)
            return bulletCount;
        return 1;
    }
    
    [Header("Parabola Settings")]
    public bool parabola = false;
    public float arcHeight = 2f; // 포물선 높이
}
