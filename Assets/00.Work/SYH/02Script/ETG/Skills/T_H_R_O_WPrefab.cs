using UnityEngine;

public class T_H_R_O_WPrefab : SkillPrefab, IPoolable
{
    Vector2 _moveDir;
    SpriteRenderer _renderer;
    Rigidbody2D _rb;
    [SerializeField] T_H_R_O_WSpriteSO _spriteSO;
    [SerializeField] string _itemName;
    public string ItemName => _itemName;

    public GameObject GameObject => gameObject;
    Debuffs _debuff;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        base.Update();
        transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime));
    } 
    
    
    private void OnEnable()
    {
        transform.position = Player.Instance.FirePos.position;
        _moveDir = SkillUtility.AimWeapon(transform);
        _rb.linearVelocity = transform.right * _shotSpeed;
    }
    public void ResetItem()
    {
        _renderer.sprite = _spriteSO._sprites[Random.Range(0, _spriteSO._sprites.Length)];
        _shotSpeed = Random.Range(1f, 20f);
        _damage = Random.Range(1f, 10f);


        int rnd = Random.Range(0, 1000);
        if (rnd < 300)
        {
            _debuff = Debuffs.Slow;
        }
        else if (rnd < 500)
        {
            _debuff = Debuffs.Bondage;
        }
        else if (rnd < 510)
        {
            _debuff = Debuffs.Marked;
        }
        else if (rnd == 1000)
        {
            _debuff = Debuffs.None;
            print("게임을 던졌습니다.");
        }
        else
        {
            _debuff = Debuffs.None;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (_debuff != Debuffs.None)
        {
            //collision.gameObject.GetComponent<DebuffController>().SetDebuff(_debuff, 2, 0.5f);
        }
        PoolManager.Instance.Push(GetComponent<IPoolable>());
    }
}
