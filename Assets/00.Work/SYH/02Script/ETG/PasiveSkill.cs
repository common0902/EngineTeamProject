using UnityEngine;

public abstract class PasiveSkill : MonoBehaviour
{
    public enum Status
    {
        None,
        Damage,
        Mana,
        ManaRecovery,
        HP,
        FullHealth,
        Speed,
        SkillCoolDown
    }

    [field: SerializeField] public Status StatusType { get; private set; }
    [field: SerializeField] public float StatusValue { get; private set; }
    [field:SerializeField]public string Name { get; private set; }
    virtual public void Take()
    {
        Destroy(gameObject);
    }
    virtual public void Effect()
    {

    }
}
