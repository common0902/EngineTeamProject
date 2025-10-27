using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace _00.Work.AJ._08._Data.BT.Action
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set Destination", story: "[Self] navigate to [NextPosition]", category: "Action", id: "4eef6e6d3951bc796159a211b763faec")]
    public partial class SetDestinationAction : Unity.Behavior.Action
    {
        [SerializeReference] public BlackboardVariable<Agent> Self;
        [SerializeReference] public BlackboardVariable<Vector3> NextPosition;
        [SerializeReference] public BlackboardVariable<bool> IsUpdate = new  BlackboardVariable<bool>(true);

        //private PathMovement _pathMovement;
        protected override Status OnStart()
        {
            if (Self.Value == null)
            {
                Debug.LogError("Self is null");
                return Status.Failure;
            }
            /*_pathMovement = Self.Value.GetCompo<PathMovement>();
            if (_pathMovement == null)
            {
                Debug.LogError("PathMovement is null");
                return Status.Failure;
            }
            _pathMovement.SetDestination(NextPosition.Value);;*/
            
            return IsUpdate == true ? Status.Running : Status.Success;
        }

        protected override Status OnUpdate()
        {
            /*if (_pathMovement.IsArrived)
            {
                return Status.Success;
            }*/
            return Status.Running;
        }

        protected override void OnEnd()
        {
        }
    }
}

