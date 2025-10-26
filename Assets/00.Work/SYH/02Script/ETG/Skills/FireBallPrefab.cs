using UnityEngine;

public class FireBallPrefab : SkillPrefab
{
    Vector2 _moveDir;
    Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _moveDir = SkillUtility.AimWeapon(transform);
        //_moveDir = _targetPos - Player.Instance.transform.position;

        //float radian = Mathf.Atan2(_moveDir.y, _moveDir.x);
        //float degree = radian * Mathf.Rad2Deg;

        //transform.eulerAngles = new Vector3(0, 0, degree);
        //if (degree > 90 || degree < -90) transform.localScale = new Vector3(1, -1, 1);
        _rb.linearVelocity = _moveDir.normalized * _shotSpeed;
    }
    //protected override void Update()
    //{
    //    base.Update();
    //}
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Destroy(gameObject);
    }
}
