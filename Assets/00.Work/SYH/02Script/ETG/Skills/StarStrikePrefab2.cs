using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class StarStrikePrefab2 : SkillPrefab, IPoolable
{
    public string ItemName => _nameString;
    [SerializeField] string _nameString;
    [SerializeField] LayerMask _skillLayer;

    public GameObject GameObject => gameObject;
    float _tarPos;
    private void Update()
    {
        if (transform.position.y <= _tarPos)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 50, _skillLayer);
            foreach (Collider2D c in colliders)
            {
                c.GetComponent<HealthSystem>().Damage(_damage + Player.Instance.PlayerStatusCompo._mana);
            }
            PoolManager.Instance.Push(GetComponent<IPoolable>());
            //gameObject.SetActive(false);
        }
        else
        {
            transform.position -= new Vector3(1, 1, 0) * _shotSpeed * Time.deltaTime;
        }
    }
    public void ResetItem()
    {
        gameObject.SetActive(true);
        _tarPos = 0;
        transform.position = new Vector3(12, 12, 0);
    }
}
