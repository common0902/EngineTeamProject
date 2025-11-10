using UnityEngine;

[CreateAssetMenu(fileName = "AutoAttackListSO", menuName = "Scriptable Objects/AutoAttackListSO")]
public class AutoAttackListSO : ScriptableObject
{
    [field:SerializeField] public Skill[] AutoAttacks { get; private set; }

}
