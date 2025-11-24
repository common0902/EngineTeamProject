using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolingList", menuName = "SO/Pool/List", order = 0)]
public class PoolingListSO : ScriptableObject
{
    public List<PoolItem> items; 
}
