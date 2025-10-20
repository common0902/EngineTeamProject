using System;
using Unity.Behavior;
using UnityEngine;

namespace _00.Work.AJ._08._Data.BT.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "CheckTargetInSight", story: "[Self] Check [Target] in sight", category: "Conditions", id: "a0b2ae874325ef663e1da794444d54e5")]
    public partial class CheckTargetInSightCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Agent> Self;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        
        public override bool IsTrue()
        {
            if (Self.Value == null)
                Debug.LogError($"Self is not set in CheckTargetInSightCondition");
            
            Vector2 origin = Self.Value.transform.position;
            Vector2 targetPosition = Target.Value.transform.position;
            Vector2 direction = targetPosition - origin;
            RaycastHit2D hitinfo =
                Physics2D.Raycast(origin, direction.normalized, direction.magnitude, Self.Value.WallisWall);
            
            return hitinfo.collider == null;
        }
    }
}
