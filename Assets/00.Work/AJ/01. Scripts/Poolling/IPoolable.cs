using UnityEngine;

public interface IPoolable
{
    public string ItemName { get; }
    public GameObject GameObject { get; }
    public void ResetItem();
}
