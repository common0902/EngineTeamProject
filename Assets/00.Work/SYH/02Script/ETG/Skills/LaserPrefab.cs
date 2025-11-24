using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class LaserPrefab : SkillPrefab
{
    LaserParentPrefab _parent;
    private void Awake()
    {
        _parent = gameObject.GetComponentInParent<LaserParentPrefab>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        print(111);
        if (collision.gameObject.CompareTag("Enemy") && !_parent._hitEnemys.Contains(collision.gameObject.GetComponent<HealthSystem>()))
        {
            print(222);
            _parent._hitEnemys.Add(collision.gameObject.GetComponent<HealthSystem>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        print(333);
        if (collision.gameObject.CompareTag("Enemy") && _parent._hitEnemys.Contains(collision.gameObject.GetComponent<HealthSystem>()))
        {
            print(444);
            _parent._hitEnemys.Remove(collision.gameObject.GetComponent<HealthSystem>());
        }
    }
}
