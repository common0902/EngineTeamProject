using UnityEngine;

public class FireBallPrefab : SkillPrefab, IPoolable
{
    Vector2 _moveDir;
    Rigidbody2D _rb;

    public string ItemName => _itemName;
    [SerializeField] string _itemName;
    [SerializeField] bool _isPiercing;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        transform.position = Player.Instance.FirePos.position;
        _moveDir = SkillUtility.AimWeapon(transform);
        _rb.linearVelocity = transform.right * _shotSpeed;
    }
    //private void Start()
    //{
    //    _moveDir = SkillUtility.AimWeapon(transform);
    //    //_moveDir = _targetPos - Player.Instance.transform.position;

    //    //float radian = Mathf.Atan2(_moveDir.y, _moveDir.x);
    //    //float degree = radian * Mathf.Rad2Deg;

    //    //transform.eulerAngles = new Vector3(0, 0, degree);
    //    //if (degree > 90 || degree < -90) transform.localScale = new Vector3(1, -1, 1);
    //    _rb.linearVelocity = _moveDir * _shotSpeed;
    //}
    //protected override void Update()
    //{
    //    base.Update();
    //}
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!_isPiercing)
        {
            PoolManager.Instance.Push(GetComponent<IPoolable>());
        }
        else
        {
            if (!collision.gameObject.CompareTag("Enemy"))
            {
                PoolManager.Instance.Push(GetComponent<IPoolable>());
            }
        }
    }

    public void ResetItem()
    {
        //transform.rotation = Quaternion.identity;
        //transform.position = Vector3.zero;
        //_rb.linearVelocity = Vector2.zero;
    }
}
