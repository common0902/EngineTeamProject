using UnityEngine;

public class Slash : Skill
{
    GameObject _prefab;
    protected override void Awake()
    {
        _prefab = Instantiate(SkillPrefab, Player.Instance.transform);
        _prefab.GetComponent<SlashPrefab>()._canAttack = true;
        _prefab.SetActive(false);
    }
    protected override void Update()
    {
        if (IsActive)
        {
            if (_waitTime >= CoolTime && _prefab.GetComponent<SlashPrefab>()._canAttack)
            {
                UseSkill();
                _waitTime -= CoolTime;
            }
        }
        _waitTime += Time.deltaTime * Player.Instance.PlayerStatusCompo._skillCoolDownSpeed / 100;
        _waitTime = Mathf.Clamp(_waitTime, 0, CoolTime);
    }
    protected override void UseSkill()
    {
        base.UseSkill();
        if(!_prefab.GetComponent<SlashPrefab>()._canAttack)
        {
            _waitTime = CoolTime;
            return;
        }
        _prefab.SetActive(true);
        _prefab.GetComponent<SlashPrefab>()._canAttack = false;
        //_prefab.transform.position = Player.Instance.FirePos.position + (Vector3)SkillUtility.AimWeapon(_prefab.transform);
    }
}