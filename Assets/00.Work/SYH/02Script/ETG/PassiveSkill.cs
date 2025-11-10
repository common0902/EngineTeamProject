using UnityEngine;

public class PassiveSkill : MonoBehaviour, IPoolable
{
    [SerializeField] PassiveSkillListSO _skillListSO;
    [SerializeField] float _deviation; //ÆíÂ÷
    [SerializeField] string _itemName;
    int _status;
    int _statusValue;
    public int Price { get; private set; }
    public PassiveSkillSO SkillSO { get; private set; }

    public string ItemName => _itemName;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        _status = Random.Range(1, _skillListSO.PassiveSkills.Length);
        SkillSO = _skillListSO.PassiveSkills[_status];
        _statusValue = Mathf.RoundToInt(Random.Range(SkillSO.StatusValue - SkillSO.StatusValue * (_deviation / 100), 
        SkillSO.StatusValue + SkillSO.StatusValue * (_deviation / 100)));
        GetComponent<SpriteRenderer>().sprite = SkillSO.SkillSprite;
        Price = Mathf.RoundToInt(Random.Range(SkillSO.Price - SkillSO.Price * (_deviation / 100),
        SkillSO.Price + SkillSO.Price * (_deviation / 100)));
    }

    public void Take()
    {
        switch(SkillSO.StatusType)
        {
            case PassiveSkillSO.Status.Damage:
                Player.Instance.PlayerStatusCompo._damage += _statusValue;
                break;
            case PassiveSkillSO.Status.MaxMana:
                Player.Instance.PlayerStatusCompo._fullMana += _statusValue;
                Player.Instance.PlayerStatusCompo._mana += _statusValue;
                break;
            case PassiveSkillSO.Status.ManaRecovery:
                Player.Instance.PlayerStatusCompo._manaRecovry += _statusValue;
                break;
            case PassiveSkillSO.Status.MaxHealth:
                Player.Instance.PlayerStatusCompo._fullHp += _statusValue;
                Player.Instance.PlayerStatusCompo._hp += _statusValue;
                break;
            case PassiveSkillSO.Status.Heal:
                Player.Instance.PlayerStatusCompo._hp += _statusValue;
                break;
            case PassiveSkillSO.Status.Speed:
                Player.Instance.PlayerStatusCompo._speed += _statusValue;
                break;
            case PassiveSkillSO.Status.SkillCoolDown:
                Player.Instance.PlayerStatusCompo._skillCoolDownSpeed += _statusValue;
                break;
        }
        Destroy(gameObject);
    }

    public void ResetItem()
    {
        throw new System.NotImplementedException();
    }
}
