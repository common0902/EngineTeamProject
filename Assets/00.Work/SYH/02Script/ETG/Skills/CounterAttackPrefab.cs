using _00.Work.SYH._02Script.ETG;
using System;
using UnityEngine;

public class CounterAttackPrefab : SkillPrefab
{
    [SerializeField] LayerMask _layer;
    private void Start()
    {
        GetComponentInChildren<CounterAttackEvent>().OnOverlab += Check;
        GetComponentInChildren<CounterAttackEvent>().OnEndCounter += Hide;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Check()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 2.75f, _layer);
        foreach (Collider2D i in enemys)
        {
            if (i.CompareTag("Enemy"))
            {
                i.GetComponent<HealthSystem>().Damage(_damage);
            }
        }
        Player.Instance.PlayerAnimationCompo.CastingEnd();
        Player.Instance.GetComponent<HealthSystem>()._isCounter = false;
        CameraHandler.Instance.Zoomout(5);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem hp))
        {
            hp.Damage(SkillUtility.CalcurateDamage(_damage));
            collision.gameObject.GetComponent<DebuffController>().SetDebuff(Debuffs.Bondage, 1.5f, 0);
        }
        GameObject effect = PoolManager.Instance.Pop("HitEffect").GameObject;
        effect.transform.position = collision.transform.position;
    }
}
