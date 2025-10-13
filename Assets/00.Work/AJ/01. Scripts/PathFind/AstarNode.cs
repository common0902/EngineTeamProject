using System;
using UnityEngine;

public class AstarNode : IComparable<AstarNode>
{
    public Vector3 worldPosition;
    public Vector3Int cellPosition;
    public NodeData nodeData;

    public AstarNode parent;
    
    public float G;
    public float F; // F = G + H
    public int CompareTo(AstarNode other)
    {
        // 비교 함수 -1, 0, 1 (같으면 0, 크면 1 작으면 -1)
        if (Mathf.Approximately(other.F, F)) return 0;
        
        return other.F < F ? -1 : 1; // 나랑 비교하는 애가 나보다 작으면
    }

    public override bool Equals(object obj)
    {
        if (obj is AstarNode astarNode)
        {
            return astarNode.cellPosition == cellPosition;
        }

        return false;
    }

    public override int GetHashCode() => cellPosition.GetHashCode();

    public static bool operator ==(AstarNode lhs, AstarNode rhs)
    {
        if (lhs is null)
        {
            if (rhs is null) return true;
            return false;
        }

        return lhs.Equals(rhs);
    }
    public static bool operator !=(AstarNode lhs, AstarNode rhs)
    {
        return !(lhs == rhs);
    }
}