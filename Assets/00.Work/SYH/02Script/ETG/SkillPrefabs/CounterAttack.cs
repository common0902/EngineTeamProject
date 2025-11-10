using UnityEngine;

public class CounterAttack : Skill
{
    GameObject _prefab;
    protected override void Awake()
    {
        base.Awake();
        _prefab = Instantiate(SkillPrefab, Player.Instance.transform);
        _prefab.SetActive(false);
    }
    protected override void Update()
    {
        //if()
        base.Update();
    }

}
