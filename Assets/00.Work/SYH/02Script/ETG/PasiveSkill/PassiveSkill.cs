using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;

public class PassiveSkill : MonoBehaviour, IPoolable
{
    [field:SerializeField] public PassiveData Data { get; private set; }
    
    [SerializeField] PassiveSkillListSO _skillListSO;
    [SerializeField] float _deviation;
    [SerializeField] string _itemName;
    public int _status;
    public int _statusValue;
    [field:SerializeField]public int Price { get; private set; }
    [field:SerializeField]public PassiveSkillSO SkillSO { get; private set; }
    TextMeshPro _text;

    public string ItemName => _itemName;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
    }

    public bool TryBuy()
    {
        if (Player.Instance.GetComponent<GoldSystem>().Gold >= Price)
        {
            Player.Instance.GetComponent<GoldSystem>().UseGold(Price);
            Take();
            return true;
        }
        return false;
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
                Player.Instance.PlayerStatusCompo.Mana += _statusValue;
                break;
            case PassiveSkillSO.Status.ManaRecovery:
                Player.Instance.PlayerStatusCompo._manaRecovry += _statusValue;
                break;
            case PassiveSkillSO.Status.MaxHealth:
                Player.Instance.PlayerStatusCompo._fullHp += _statusValue;
                Player.Instance.PlayerStatusCompo.Hp += _statusValue;
                break;
            case PassiveSkillSO.Status.Heal:
                Player.Instance.PlayerStatusCompo.Hp += _statusValue;
                break;
            case PassiveSkillSO.Status.Speed:
                Player.Instance.PlayerStatusCompo._speed += _statusValue;
                break;
            case PassiveSkillSO.Status.SkillCoolDown:
                Player.Instance.PlayerStatusCompo._skillCoolDownSpeed += _statusValue;
                break;
        }
        PoolManager.Instance.Push(this);
    }

    public void ResetItem()
    {
        _status = Random.Range(0, _skillListSO.PassiveSkills.Length);
        SkillSO = _skillListSO.PassiveSkills[_status];
        _statusValue = Mathf.RoundToInt(Random.Range(SkillSO.StatusValue - SkillSO.StatusValue * (_deviation / 100),
        SkillSO.StatusValue + SkillSO.StatusValue * (_deviation / 100)));
        GetComponent<SpriteRenderer>().sprite = SkillSO.SkillSprite;
        Price = Mathf.RoundToInt(Random.Range(SkillSO.Price - SkillSO.Price * (_deviation / 100),
        SkillSO.Price + SkillSO.Price * (_deviation / 100)));

        Data = SkillSO.Data;

        _text.text = Price.ToString();
    }
}
