using System.Collections.Generic;
using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

public class LaserParentPrefab : SkillPrefab
{
    public List<HealthSystem> _hitEnemys = new List<HealthSystem>();
    [SerializeField] LayerMask _layer;
    Transform _tail;
    private void Awake()
    {
        _tail = transform.GetChild(1);
    }
    public void Attack()
    {
        //if (_hitEnemys.Count == 0) return;
        try
        {
            print(555);
            SoundManager.Instance.PlaySound("UseSkill");
            foreach (HealthSystem i in _hitEnemys)
            {
                print(666);
                i.Damage(SkillUtility.CalcurateDamage(_damage));
                GameObject effect = PoolManager.Instance.Pop("HitEffect").GameObject;
                effect.transform.position = i.transform.position;
                if (i.gameObject == null)
                {
                    _hitEnemys.Remove(i);
                }
            }
        }
        catch
        {
            
        }        //transform.GetChild(0).GetComponent<LaserPrefab>().Attack();
        //_tail.GetComponent<LaserPrefab>().Attack();
    }
    protected override void Update()
    {
        transform.position = Player.Instance.FirePos.position;
        Vector2 targetPos = SkillUtility.AimWeapon(transform);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos, 20, _layer);

        Vector2 endPos = hit.point;
        if (endPos == Vector2.zero)
        {
            _tail.localPosition = new Vector3(10.5f, _tail.localPosition.y, 0);
            _tail.localScale = new Vector3(20, 1, 1);
            return;
        }
        float distance = Vector3.Distance(transform.position + new Vector3(0.5f, 0, 0), endPos);
        _tail.localPosition = new Vector3(distance / 2 + 0.5f, _tail.localPosition.y, 0);
        _tail.localScale = new Vector3(distance, 1, 1);
    }
}
