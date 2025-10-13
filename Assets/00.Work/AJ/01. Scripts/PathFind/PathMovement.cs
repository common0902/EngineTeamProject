using System;
using System.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PathAgent))]
public class PathMovement : MonoBehaviour, IComponent
{
    [SerializeField] private PathAgent pathAgent;
    [SerializeField] private int maxPathCount = 100;
    [SerializeField] private Tilemap baseTilemap;

    private Vector3[] _path;
    private int _currentPathIndex;
    private int _pathCount;
    private Vector3 _nextPoint;
    private Vector2 _beforePosition;
    
    private Agent _owner;    
    private AgentMovement _mover;
    
    public bool IsArrived { get; private set; }
    public bool IsPathFailed { get; private set; }
    public bool IsPathPending { get; private set; }
    public bool IsStop { get; set; }
    
    [SerializeField] private Transform target;

    [ContextMenu("Sample")]
    public void Sample()
    {
        SetDestination(target.position);
    }
    
    public void Initialize(Agent agent)
    {
        _owner = agent;
        if(agent == null || _owner == null) Debug.LogError("Agent or Owner is null");
        _path = new Vector3[maxPathCount];
        _mover = _owner.GetCompo<AgentMovement>();
        if(_mover == null) Debug.LogError("AgentMovement is null");
    }
    
    public void SetDestination(Vector3 destination)
    {
        Vector3Int startCell = baseTilemap.WorldToCell(transform.position);
        Vector3Int endCell = baseTilemap.WorldToCell(destination);
        IsArrived = false;
        IsPathFailed = false;
        IsPathPending = true;
        _beforePosition = transform.position; //　맨 처음 위치 이전 좌표로 저장.
        
        _pathCount = pathAgent.GetPath(startCell, endCell, _path);

        if (_pathCount <= 1)
        {
            IsPathFailed = true;
        }
        else
        {
            _currentPathIndex = 0;
        }

        IsPathPending = false;
    }
    private void Update()
    {
        if (IsStop) return;
        if (_currentPathIndex >= _pathCount) return; // 경로가 끝남.
        if (CheckArrived() == false)
        {
            Vector2 direction = _path[_currentPathIndex] - transform.position;
            _mover.SetMovementInput(direction);
        }
        else
        {
            _mover.StopImmediately();
        }
    }

    private bool CheckArrived()
    {
        Vector2 destination = _path[_currentPathIndex];
        Vector2 currentPosition = transform.position;
        Vector2 beforePosition = (destination - currentPosition).normalized;
        Vector2 currentDirection = (destination - currentPosition).normalized;
        _beforePosition = currentPosition;

        if (Vector2.Dot(beforePosition, currentDirection) <= 0 
            || Vector2.Distance(destination, currentPosition) < 0.01f)
        {
            _currentPathIndex++;
            if (_currentPathIndex >= _pathCount)
                IsArrived = true;
            return IsArrived;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (_pathCount <= 0)
        {
            return;
        };

        for (int i = 0; i < _pathCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_path[i], 0.25f);
            Gizmos.DrawLine(_path[i], _path[i+1]);
        }

        Gizmos.DrawSphere(_path[_pathCount - 1], 0.25f);
    }

}
