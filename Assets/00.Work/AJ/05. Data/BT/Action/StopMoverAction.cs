using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace _00.Work.AJ._08._Data.BT.Action
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Stop mover", story: "[Self] stop mover", category: "Action", id: "16abe38a947db80b3d48d2edd6028863")]
    public partial class StopMoverAction : Unity.Behavior.Action
    {
        [SerializeReference] public BlackboardVariable<Agent> Self;

        protected override Status OnStart()
        {
            if (Self.Value == null)
            {
                Debug.LogError("Self is not set in  StopMoverAction");
                return Status.Failure;
            }
            AgentMovement mover = Self.Value.GetCompo<AgentMovement>();
            if(mover == null)
            {
                Debug.LogError("AgentMovement is not set in StopMoverAction");
                return Status.Failure;
            }
            
            mover.StopImmediately();
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}

