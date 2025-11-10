using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkillListSO", menuName = "Scriptable Objects/PassiveSkillListSO")]
public class PassiveSkillListSO : ScriptableObject
{
    [field: SerializeField] public PassiveSkillSO[] PassiveSkills { get; private set; }
}
