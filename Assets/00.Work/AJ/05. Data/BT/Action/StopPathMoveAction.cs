using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace _00.Work.AJ._08._Data.BT.Action
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Stop path move", story: "[Self] stop pathMove to [NewValue]", category: "Action/Path", id: "f689001e3f2793f181f8fc55989dd57a")]
    public partial class StopPathMoveAction : Unity.Behavior.Action
    {
        [SerializeReference] public BlackboardVariable<Agent> Self;
        [SerializeReference] public BlackboardVariable<bool> NewValue;

        protected override Status OnStart()
        {
            if (Self.Value == null)
            {
                Debug.LogError("Self is not set in StopPathMoveAction");
                return Status.Failure;
            } 
            //PathMovement movement = Self.Value.GetCompo<PathMovement>();
            /*if (movement == null)
            {
                Debug.LogError("PathMovement component not found on Self in StopPathMoveAction.");
                return Status.Failure;
            }

            movement.IsStop = NewValue.Value;*/
            
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

