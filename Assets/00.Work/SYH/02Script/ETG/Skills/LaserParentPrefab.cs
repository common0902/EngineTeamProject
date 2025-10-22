using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserParentPrefab : SkillPrefab
{
    public List<GameObject> _hitEnemys = new List<GameObject>();
    public Action<int> OnHitWall;
    [SerializeField] LayerMask _layer;
    Transform _tail;
    private void Awake()
    {
        _tail = transform.GetChild(1);
        OnHitWall += LaserHide;
    }

    private void LaserHide(int value)
    {
        
    }
    public void Attack()
    {

    }
    protected override void Update()
    {
        Vector2 targetPos = SkillUtility.AimWeapon(transform);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos * 20, 0, _layer);
        try
        {
            Vector2 endPos = hit.point;
            //float distance=
        }
        catch
        {

        }
    }
}
