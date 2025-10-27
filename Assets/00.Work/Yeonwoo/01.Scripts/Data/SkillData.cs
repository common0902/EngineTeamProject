using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.Data
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Player/SkillData")]
    public class SkillData : ScriptableObject
    {
        [Header("References")]
        [field:SerializeField] public Skill Skill { get; private set; }
        
        [Header("Information")]
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public string Description { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        
        //[Header("Stats")] 
        //public SerializedDictionary<EntityStat, float> stats; // EntityStat은 임시임, 나중에 스킬/패시브 통합 스탯 SO 만들면 그 때 바꾸기
    }
}