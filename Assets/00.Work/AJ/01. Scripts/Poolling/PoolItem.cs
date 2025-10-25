using UnityEngine;

[CreateAssetMenu(fileName = "PoolObject", menuName = "SO/Pool/Object", order = 0)]
public class PoolItem : ScriptableObject
{
    public string poolName;
    public GameObject prefab;
    public int count;

    private void OnValidate()
    {
        if (prefab == null) return;
        IPoolable item = prefab.GetComponent<IPoolable>();
        if (item == null)
        {
            Debug.LogWarning($"Can not find IPoolable in {prefab.name}");
            prefab = null;
            return;
        }

        poolName = item.ItemName;
    }
}
