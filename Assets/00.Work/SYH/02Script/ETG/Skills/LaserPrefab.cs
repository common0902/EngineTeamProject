using System.Collections.Generic;
using UnityEngine;

public class LaserPrefab : SkillPrefab
{
    LaserParentPrefab _parent;
    [SerializeField] int _num;
    private void Awake()
    {
        _parent = gameObject.GetComponentInParent<LaserParentPrefab>();
    }
    protected override void Update()
    {
        
    }
    public void Attack()
    {
        foreach (GameObject i in _parent._hitEnemys)
        {
            print(i + "¸ÂÀ½");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_parent._hitEnemys.Contains(collision.gameObject))
        {
            _parent._hitEnemys.Add(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            _parent.OnHitWall.Invoke(_num);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _parent._hitEnemys.Contains(collision.gameObject))
        {
            _parent._hitEnemys.Remove(collision.gameObject);
        }
    }
}
