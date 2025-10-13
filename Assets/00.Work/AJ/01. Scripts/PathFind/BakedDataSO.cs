using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BakedData", menuName = "SO/Path/BakedData")]
public class BakedDataSO : ScriptableObject
{
    public List<NodeData> points = new List<NodeData>();

    private Dictionary<Vector3Int, NodeData> _pointDict; // 빠르게 포인트를 찾기 위해서

    private void OnEnable()
    {
        Initalize();
    }

    private void Initalize()
    {
        if (_pointDict == null || _pointDict.Count != points.Count)
            _pointDict = points.ToDictionary(node => node.cellPosition); 
    }

    public void ClearPoints() => points?.Clear(); 

    public void AddPoint(Vector3 worldPosition, Vector3Int cellPosition) // SO에 포인트를 추가하는 코드
    {
        points.Add(new NodeData(worldPosition, cellPosition));;   
    }
    
    public bool HasNode(Vector3Int cellPosition) // 현재 그 노드가 있는지 검사하는 코드
        => _pointDict != null && _pointDict.ContainsKey(cellPosition); // Dictionary에 해당 키가 존재하는가?

    public bool TryGetNode(Vector3Int cellPosition, out NodeData nodeData)
    {
        if (HasNode(cellPosition)) // HasNode를 기반으로 해 
        {
            nodeData = _pointDict[cellPosition]; // 있으면 NodeData를 주고
            return true;
        }
        
        nodeData = default;
        return false; // 없으면 안주고
    }
}
