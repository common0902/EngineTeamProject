using System;
using UnityEngine;

[Serializable]
public struct LinkData
{
    public Vector3 startPosition;
    public Vector3Int startCellPosition;
    public Vector3 endPosition;
    public Vector3Int endCellPosition;

    public float cost;
}
