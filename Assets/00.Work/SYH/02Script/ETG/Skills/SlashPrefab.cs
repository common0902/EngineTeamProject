using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
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
        Collider2D[] enemys = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 0.5f, transform.position.y), new Vector2(4, 5),0, SkillUtility.GetEnemyLayer());
        if (enemys == null)
        {
            SoundManager.Instance.PlaySound("Slash");
        }
        else
        {
            SoundManager.Instance.PlaySound("Slash_Hit");
            foreach (Collider2D i in enemys)
            {
                if (i.CompareTag("Enemy"))
                {
                    i.GetComponent<HealthSystem>().Damage(_damage);
                    GameObject effect = PoolManager.Instance.Pop("HitEffect").GameObject;
                    effect.transform.position = i.transform.position;
                }
            }
        }
    }

    public void Hide()
    {
        _flip = -_flip;
        transform.localScale = new Vector3(1, _flip, 1);
        _canAttack = true;
        gameObject.SetActive(false);
    }
    protected override void Update()
    {
        transform.localPosition = _pos;
    }
}