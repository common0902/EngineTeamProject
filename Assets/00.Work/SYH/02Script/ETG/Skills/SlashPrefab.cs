using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class SlashPrefab : SkillPrefab
{
    public bool _canAttack;
    int _flip = 1;  
    Vector3 _pos;
    [SerializeField] AudioClip _failedSound;
    private void OnEnable()
    {
        _pos = 1.5f * (Vector3)SkillUtility.AimWeapon(transform);
        Collider2D[] enemys = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 0.5f, transform.position.y), new Vector2(4, 5),0);
        if (enemys == null)
        {

        }
        else
        {
            foreach (Collider2D i in enemys)
            {
                if (i.CompareTag("Enemy"))
                {
                    i.GetComponent<HealthSystem>().Damage(_damage);
                }
            }
        }
    }

    public void Hide()
    {
        transform.localScale = new Vector3(1, _flip, 1);
        _canAttack = true;
        _flip = -_flip;
        gameObject.SetActive(false);
    }
    protected override void Update()
    {
        transform.localPosition = _pos;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem hp))
        {
            hp.Damage(SkillUtility.CalcurateDamage(_damage));
        }
        GameObject effect = PoolManager.Instance.Pop("HitEffect").GameObject;
        effect.transform.position = collision.transform.position;
    }
}