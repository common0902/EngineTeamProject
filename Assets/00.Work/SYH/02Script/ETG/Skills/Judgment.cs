using _00.Work.SYH._02Script.ETG;
using System.Collections;
using Unity.AppUI.UI;
using UnityEngine;

public class Judgment : Skill
{
    [SerializeField] float _radious;
    [SerializeField] GameObject _guideLine;
    protected override void Awake()
    {
        base.Awake();
        _guideLine = Instantiate(_guideLine, Player.Instance.transform);
        _guideLine.SetActive(false);
    }
    protected override void UseSkill()
    {
        base.UseSkill();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Player.Instance.transform.position, _radious, SkillUtility.GetEnemyLayer());
        for (int i = 0; i < colliders.Length; i++)
        {
            print(colliders[i].name);
            StartCoroutine(Wait(i * 0.1f, colliders[i].transform.position));
        }
    }
    public override void Passive()
    {
        print(111);
        _guideLine.SetActive(true);
    }
    public override void DisPassive()
    {
        _guideLine.SetActive(false);
    }

    IEnumerator Wait(float time, Vector2 pos)
    {
        yield return new WaitForSeconds(time);
        GameObject prefab = PoolManager.Instance.Pop(Name).GameObject;
        prefab.transform.position = pos;
    }
}
