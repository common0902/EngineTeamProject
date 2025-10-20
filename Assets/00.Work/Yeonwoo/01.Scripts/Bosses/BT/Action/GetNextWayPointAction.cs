using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Bosses.BT.Action
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Get next wayPoint", story: "get [NextPathPoint] from [WayPoints]", category: "Action/Path", id: "37e9858ee2a8d9690d979a0d52d68dd9")]
    public partial class GetNextWayPointAction : Unity.Behavior.Action
    {
        [SerializeReference] public BlackboardVariable<Vector3> NextPathPoint;
        [SerializeReference] public BlackboardVariable<WayPoints> WayPoints;

        protected override Status OnStart()
        {
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

