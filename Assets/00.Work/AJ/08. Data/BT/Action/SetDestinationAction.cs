using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace _00.Work.AJ._08._Data.BT.Action
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set destination", story: "[Self] navigate to [NextPosition]", category: "Action/Path", id: "fced91c8061f6c417edb4ef504d0419a")]
    public partial class SetDestinationAction : Unity.Behavior.Action
    {
        [SerializeReference] public BlackboardVariable<Agent> Self;
        [SerializeReference] public BlackboardVariable<Vector3> NextPosition;

        private PathMovement _pathMovement;
        protected override Status OnStart()
        {
            if (Self.Value == null)
            {
                Debug.LogError("Self is not set in SetDestinationAction");
                return Status.Failure;
            }
            _pathMovement = Self.Value.GetCompo<PathMovement>();
            if (_pathMovement == null)
            {
                Debug.LogError("PathMovement component is not found on Self in SetDestinationAction");
                return Status.Failure;
            }
            _pathMovement.SetDestination(NextPosition.Value);
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_pathMovement.IsArrived)
            {
                return Status.Success;
            }

            return Status.Running;
        }
    }
}

