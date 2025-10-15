using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Data
{
    [CreateAssetMenu(fileName = "EntityStat", menuName = "Entity/EntityStat")]
    public class EntityStat : ScriptableObject
    {
        [field: SerializeField] public float Health { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; }
        [field:SerializeField] public float MinDamage { get; set; }
        [field: SerializeField] public float Damage { get; set; }
        [field: SerializeField] public float MaxDamage { get; set; }
        [field: SerializeField] public float MinSpeed { get; set; }
        [field:SerializeField] public float Speed { get; set; }
    }
}