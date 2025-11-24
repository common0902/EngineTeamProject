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
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        transform.position = Player.Instance.FirePos.position;
        SkillUtility.AimWeapon(transform);
        SkillUtility.CalculateAngle(transform, _spreadAngle);
        _rb.linearVelocity = transform.right * _shotSpeed;
    }
    public void ResetItem()
    {
        //transform.rotation = Quaternion.identity;
        //transform.position = Vector3.zero;
        //_rb.linearVelocity = Vector2.zero;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        PoolManager.Instance.Push(GetComponent<IPoolable>());
        gameObject.SetActive(false);
    }
}
