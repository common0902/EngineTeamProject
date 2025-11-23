using UnityEngine;
using System.Collections.Generic;
using _00.Work.SYH._02Script.ETG;

public class BeamPrefab : SkillPrefab
{
    public List<HealthSystem> _hitEnemys = new List<HealthSystem>();
    [SerializeField] LayerMask _layer;
    Animator _ani;
    CircleCollider2D _collider;
    readonly int _endHash = Animator.StringToHash("End");
    [SerializeField] bool _canMove;
    [SerializeField] float _bondageTime;
    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();
        _collider.enabled = false;
    }
    private void OnEnable()
    {
        transform.position = MouseInput.Instance.MousePosition + new Vector2(0.05f, 4.5f);
    }
    protected override void Update()
    {
        if (_canMove)
        {
            transform.position += (Vector3)SkillUtility.AimWeapon((Vector2)transform.position - new Vector2(0.05f, 4.5f)) * Time.deltaTime * _shotSpeed;
        }
    }
    public void Attack()
    {
        try
        {
            foreach (HealthSystem i in _hitEnemys)
            {
                i.Damage(SkillUtility.CalcurateDamage(_damage));
                i.GetComponent<DebuffController>().SetDebuff(Debuffs.Bondage, _bondageTime, 0);
                if (i.gameObject == null)
                {
                    _hitEnemys.Remove(i);
                }
            }
        }
        catch
        {
            Attack();
        }
    }
    public void StartAttack()
    {
        _collider.enabled = true;
        _canMove = true;
    }
    public void EndAttack()
    {
        _ani.SetTrigger(_endHash);
        _collider.enabled = false;
        _canMove = false;
    }
    public void End()
    {
        gameObject.SetActive(false);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_hitEnemys.Contains(collision.gameObject.GetComponent<HealthSystem>()))
        {
            _hitEnemys.Add(collision.gameObject.GetComponent<HealthSystem>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _hitEnemys.Contains(collision.gameObject.GetComponent<HealthSystem>()))
        {
            _hitEnemys.Remove(collision.gameObject.GetComponent<HealthSystem>());
            GameObject effect = PoolManager.Instance.Pop("HitEffect").GameObject;
            effect.transform.position = transform.position;
        }
    }
}
