using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace _00.Work.AJ._08._Data.BT.Action
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set nextPosition", story: "Set [NextPosition] from [Target]", category: "Action/Path", id: "56b3094392effde3461baa1d52e75517")]
    public partial class SetNextPositionAction : Unity.Behavior.Action
    {
        [SerializeReference] public BlackboardVariable<Vector3> NextPosition;
        [SerializeReference] public BlackboardVariable<Transform> Target;

        protected override Status OnStart()
        {
            if (Target.Value == null)
            {
                Debug.LogError("Target is not set in SetNextPositionAction");
                return Status.Failure;
            }
            
            NextPosition.Value = Target.Value.position;
            return Status.Running;
        }
    }
}

