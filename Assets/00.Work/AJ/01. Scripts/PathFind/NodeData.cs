using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct NodeData
{
    public Vector3 worldPosition;
    public Vector3Int cellPosition;
    public List<LinkData> neighbors; // 이웃을 가지고 있어야 하니깐

    public NodeData(Vector3 worldPosition, Vector3Int cellPosition)
    {
        this.worldPosition = worldPosition;
        this.cellPosition = cellPosition;
        neighbors = new List<LinkData>();
    }

    public void AddNeighbor(NodeData neighborNode)
    {
        neighbors.Add(new LinkData
        {
            startPosition = worldPosition,
            startCellPosition = cellPosition,
            endPosition = neighborNode.worldPosition,
            endCellPosition = neighborNode.cellPosition,
            cost = Vector3Int.Distance(cellPosition, neighborNode.cellPosition)
        });
    }
    
    public override int GetHashCode() => cellPosition.GetHashCode();
    
    public override bool Equals(object obj)
    {
        if (obj is NodeData nodeData) // 오브젝트가 노드데이터이면
        {
            return nodeData.cellPosition == cellPosition; // 노드 데이터의 cellPos와 나의 cellPos가 같다면
        }

        return false;
    }

    public static bool operator ==(NodeData lhs, NodeData rhs) // operator는 예약어, ==을 재정의 한거 
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(NodeData lhs, NodeData rhs)
    {
        return !(lhs == rhs);
    }
}