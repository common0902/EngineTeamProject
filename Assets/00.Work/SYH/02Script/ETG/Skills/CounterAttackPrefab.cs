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
}
