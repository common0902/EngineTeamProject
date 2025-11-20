using _00.Work.SYH._02Script.ETG;
using System;
using UnityEngine;

public class MarkerPrefab : FireBallPrefab
{
    [SerializeField] float _markerValue;
    public Action OnKillMarker;
    HealthSystem _aaa;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {   
            collision.gameObject.GetComponent<DebuffController>().SetDebuff(Debuffs.Marked, 0, _markerValue);
            _aaa = collision.gameObject.GetComponent<HealthSystem>();
            collision.gameObject.GetComponent<HealthSystem>().OnDead += KillMarker;
        }

        base.OnTriggerEnter2D(collision);
    }
    void KillMarker()
    {
        _aaa.OnDead -= KillMarker;
        OnKillMarker?.Invoke();
    }
}
