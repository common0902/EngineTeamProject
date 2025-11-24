using UnityEngine;

public class HitEffect : MonoBehaviour, IPoolable
{
    public string ItemName => _itemName;
    [SerializeField] string _itemName;
    public GameObject GameObject => gameObject;

    public void Push()
    {
        PoolManager.Instance.Push(this);
    }

    public void ResetItem()
    {

    }
}
