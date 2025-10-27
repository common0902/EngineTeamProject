using UnityEngine;

public class GatlingPrefab : SkillPrefab, IPoolable
{
    public string ItemName => _itemName;
    [SerializeField] string _itemName;
    [SerializeField] float _spreadAngle;
    Rigidbody2D _rb;
    public GameObject GameObject => gameObject;

    private void Awake()
    {
        SkillUtility.AimWeapon(transform);
        SkillUtility.CalculateAngle(transform, _spreadAngle);
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = transform.right * _shotSpeed;
    }
    public void ResetItem()
    {
        
    }
}
