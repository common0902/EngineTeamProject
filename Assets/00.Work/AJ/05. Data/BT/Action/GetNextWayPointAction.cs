using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace _00.Work.AJ._08._Data.BT.Action
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Get next wayPoint", story: "Get [NextPathPoint] from [WayPoints]", category: "Action/Path", id: "68cb0ce33058e99637f4e7cb83635740")]
    public partial class GetNextWayPointAction : Unity.Behavior.Action
    {
        [SerializeReference] public BlackboardVariable<Vector3> NextPathPoint;
        [SerializeReference] public BlackboardVariable<WayPoints> WayPoints;

        protected override Status OnStart()
        {
            NextPathPoint.Value = WayPoints.Value.GetNextWayPoint();
            return Status.Success;
        }
    }
}

