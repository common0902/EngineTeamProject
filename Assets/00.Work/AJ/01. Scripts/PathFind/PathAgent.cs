using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    [SerializeField] private BakedDataSO bakedData;

    private PriorityQueue<AstarNode> _openList = new PriorityQueue<AstarNode>();
    private List<AstarNode> _closeList = new List<AstarNode>();
    private List<AstarNode> _path = new List<AstarNode>(); // 실제 경로 저장.
    
    public int GetPath(Vector3Int startPosition, Vector3Int destination, Vector3[] pointArr)
    {
        // F(비용값) = G(실제거리) + H(추상적 거리)
        if (CaculatePath(startPosition, destination))
        {
            int cornerIdx = 0;
            pointArr[cornerIdx] = _path[0].worldPosition;
            cornerIdx++;

            for (int i = 1; i < _path.Count - 1; i++)
            {
                if (cornerIdx >= pointArr.Length) break;

                Vector3Int beforeDirection = _path[i].cellPosition - _path[i - 1].cellPosition;
                Vector3Int nextDirection = _path[i + 1].cellPosition - _path[i].cellPosition;

                if (beforeDirection != nextDirection)
                {
                    pointArr[cornerIdx] = _path[i].worldPosition;
                    cornerIdx++;
                }
            }
            
            pointArr[cornerIdx] = _path[^1].worldPosition;
            cornerIdx++;
            
            return cornerIdx;
        }
        return 0;
    }

    private bool CaculatePath(Vector3Int startPosition, Vector3Int destination)
    {
        _openList.Clear();
        _closeList.Clear();
        _path.Clear();

        bool result = false; // 길을 찾았는가?
        if (bakedData.TryGetNode(startPosition, out NodeData startNode) == false)
            return false;

        if (bakedData.TryGetNode(destination, out NodeData endNode) == false)
            return false;
        
        _openList.Push(new AstarNode
        {
            nodeData= startNode,
            cellPosition = startNode.cellPosition,
            worldPosition = startNode.worldPosition,
            parent = null,
            G = 0, F = CalcH(startNode.cellPosition, endNode.cellPosition)
        });
        
        while (_openList.Count > 0)
        {
            AstarNode currentNode = _openList.Pop(); // 가장 비용이 낮은애들을 팝
            foreach (LinkData link in currentNode.nodeData.neighbors)
            {
                bool isVisited = _closeList.Any(node => node.cellPosition == link.endCellPosition); // 이 조건을 만족하는 하나라도 있으면 True, 아니면 False
                if (isVisited) continue; // 이미 방문한 노드의 경우는 continue로 버린다.

                if (bakedData.TryGetNode(link.endCellPosition, out NodeData nextNode) == false)
                    continue; // 해당 노드가 존재하지 않을 경우도 버린다.

                float newG = link.cost + currentNode.G; // 현재까지 경로값고 새로운 경로값을 더한다.
                
                AstarNode nextAstarNode = new AstarNode
                {
                    nodeData = nextNode,
                    cellPosition = nextNode.cellPosition,
                    worldPosition = nextNode.worldPosition,
                    parent = currentNode,
                    G = newG,
                    F = newG + CalcH(nextNode.cellPosition, endNode.cellPosition)
                };
                AstarNode existInOpenList = _openList.Contains(nextAstarNode);
                if (existInOpenList != null)
                {
                    // 새롭게 발견된 경로값이 더 적을 경우에는 교체해준다.
                    if (nextAstarNode.G < existInOpenList.G)
                    {
                        existInOpenList.G = nextAstarNode.G;
                        existInOpenList.F = nextAstarNode.F;
                        existInOpenList.parent = nextAstarNode.parent;
                    }
                }
                else
                {
                    _openList.Push(nextAstarNode);
                }
                // end of foreach
            }
            _closeList.Add(currentNode);
            if (currentNode.nodeData == endNode)
            {
                result = true;
                break;
            }
        }

        if (result)
        {
            AstarNode last = _closeList[^1]; // 마지막 원소를 가리킴
            while (last.parent != null)
            {
                _path.Add(last);
                last = last.parent;
            }
            _path.Add(last); // 시작점을 넣어주고
            _path.Reverse(); // 거꾸로 뒤집는다.
        }

        return result;
    }

    private float CalcH(Vector3Int startNodeCellPosition, Vector3Int endCellPosition)
    {
        return Vector3Int.Distance(startNodeCellPosition, endCellPosition);
    }
}