using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkillSO", menuName = "Scriptable Objects/PassiveSkillSO")]
public class PassiveSkillSO : ScriptableObject
{
    public enum Status
    {
        None,
        Damage,
        MaxMana,
        ManaRecovery,
        Heal,
        MaxHealth,
        Speed,
        SkillCoolDown
    }
    [Header("Passive Info")]
    [field: SerializeField] public PassiveData Data { get; private set; }

    [field: SerializeField] public Status StatusType { get; private set; }
    [field: SerializeField] public float StatusValue { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public Sprite SkillSprite { get; private set; }
    [field:SerializeField]public string Name { get; private set; }

    private void OnValidate()
    {
        Name = this.name;
    }
}
