using UnityEngine;

public class Laser : Skill
{
    GameObject _prefab;
    private void Awake()
    {
        _prefab = Instantiate(SkillPrefab, Player.Instance.FirePos.position, Quaternion.identity);
        _prefab.SetActive(false);
    }
    private void Start()
    {
        Player.Instance.SkillControllerCompo.OnChangeSkill += DisActive;
    }
    public override void Active()
    {
        base.Active();
        _prefab.SetActive(true);
    }
    public override void DisActive()
    {
        base.DisActive();
        _prefab.SetActive(false);
    }
    protected override void UseSkill()
    {
        base.UseSkill();
        _prefab.GetComponent<LaserParentPrefab>().Attack();
    }
}
