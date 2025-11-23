using UnityEngine;

public class ItemSeller : MonoBehaviour
{
    [SerializeField]Transform[] _itemPoses;
    [SerializeField] string _passiveItemName = "Potion";
    private void Start()
    {
        RoomManager.Instance.OnInPortal += () =>
        {
            foreach (Transform i in _itemPoses)
            {
                GameObject item = PoolManager.Instance.Pop(_passiveItemName).GameObject;
                item.transform.position = i.position;
            }
        };

        
    }
}
