using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class StarStrikePrefab2 : SkillPrefab, IPoolable
{
    public string ItemName => _nameString;
    [SerializeField] string _nameString;

    public GameObject GameObject => gameObject;
    float _tarPos;
    protected override void Update()
    {
        if (transform.position.y <= _tarPos)
        {
            SkillUtility.WideAreaDamage(_damage + Player.Instance.PlayerStatusCompo.Mana / 5f, 15, SkillUtility.GetEnemyLayer());
            SkillUtility.WideAreaDamage(_damage + Player.Instance.PlayerStatusCompo.Mana / 5f, 15, SkillUtility.GetEnemyLayer(), Debuffs.Slow, 2.5f, 0.5f);
            PoolManager.Instance.Push(GetComponent<IPoolable>());
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
        transform.position = new Vector3(12, 12, 0) + (Vector3)StarStrike.Instance._pos; ;
    }
}
