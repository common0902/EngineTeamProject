using Unity.Behavior;

namespace _00.Work.AJ._01._Scripts.Enemy
{
    [BlackboardEnum]
    public enum EnemyStates
    {
        PATROL = 0,
        CHASE = 1,
        ATTACK = 2
    }
}