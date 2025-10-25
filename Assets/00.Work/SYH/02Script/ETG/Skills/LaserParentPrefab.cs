using System.Collections.Generic;
using UnityEngine;

public class LaserParentPrefab : SkillPrefab
{
    public List<GameObject> _hitEnemys = new List<GameObject>();
    [SerializeField] LayerMask _layer;
    Transform _tail;
    private void Awake()
    {
        _tail = transform.GetChild(1);
    }
    public void Attack()
    {
        transform.GetChild(0).GetComponent<LaserPrefab>().Attack();
        _tail.GetComponent<LaserPrefab>().Attack();
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
