using System;
using UnityEngine;

public class ItemSeller : MonoBehaviour
{
    [SerializeField]Transform[] _itemPoses;
    [SerializeField] string _passiveItemName = "Potion";
    GameObject[] Potions = new GameObject[3];
    public event Action OnPushPotion;
    private void Start()
    {
        //print(222);
        RoomManager.Instance.OnInPortal += Resett;
    }
    private void Resett()
    {
            //print(999);
            RoomManager.Instance.OnInPortal -= Resett;
            //print(1010);
            for (int i = 0; i < _itemPoses.Length; i++)
            {
                //print("Start i = " + i);
                Potions[i] = PoolManager.Instance.Pop(_passiveItemName).GameObject;
                //print(Potions[i].transform.position);
                //print(_itemPoses);
                //print(_itemPoses[i]);
                //print(_itemPoses[i].position);
                Potions[i].transform.position = _itemPoses[i].position;
                //print(666);
                Potions[i].SetActive(true);
                OnPushPotion += Potions[i].GetComponent<PassiveSkill>().Push;
                //print(777);
            }
        
        
    }
    private void OnDestroy()
    {
        //print(111);
        //RoomManager.Instance.OnInPortal -= Resett;
        OnPushPotion?.Invoke();
    }
}
