using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserParentPrefab : SkillPrefab
{
    public List<GameObject> _hitEnemys = new List<GameObject>();
    public Action<int> OnHitWall;
    private void Awake()
    {
        OnHitWall += LaserHide;
    }

    private void LaserHide(int value)
    {
        for (int i = value; i < transform.childCount; i++)
        {

        }
    }

    protected override void Update()
    {
        SkillUtility.AimWeapon(transform);
    }
}
