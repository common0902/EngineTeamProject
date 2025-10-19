using UnityEngine;

public class Player : MonoSingleton<Player>
{
    [field: SerializeField] public SkillController SkillControllerCompo { get; private set; }
    [field: SerializeField] public PasiveSkillController PasiveSkillControllerCompo { get; private set; }
    [field: SerializeField] public PlayerStatus PlayerStatusCompo { get; private set; }
}
