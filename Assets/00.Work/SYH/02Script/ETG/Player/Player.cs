using _00.Work.SYH._02Script.ETG;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    [field: SerializeField] public SkillController SkillControllerCompo { get; private set; }
    [field: SerializeField] public PassiveSkillController PassiveSkillControllerCompo { get; private set; }
    [field: SerializeField] public PlayerStatus PlayerStatusCompo { get; private set; }
    [field: SerializeField] public PlayerMove PlayerMoveCompo { get; private set; }
    [field: SerializeField] public PlayerAnimation PlayerAnimationCompo { get; private set; }
    [field:SerializeField]public Transform FirePos { get; private set; }
    [field:SerializeField]public HealthSystem PlayerHealthSystemCompo { get; private set; }
}
