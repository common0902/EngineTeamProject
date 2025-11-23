using UnityEngine;

public class JudgmentPrefab : SkillPrefab, IPoolable
{
    public string ItemName => _itemName;
    [SerializeField] string _itemName;
    public GameObject GameObject => gameObject;

    public void ResetItem()
    {

    }
    public void Push()
    {
        PoolManager.Instance.Push(this);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision != null)
        {
            collision.gameObject.GetComponent<DebuffController>().SetDebuff(Debuffs.Slow, 1, 0.5f);
        }
    }
}
