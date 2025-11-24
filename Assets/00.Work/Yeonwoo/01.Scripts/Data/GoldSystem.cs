using System;
using _00.Work.Yeonwoo._01.Scripts.UI;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Data
{
    public class GoldSystem : MonoBehaviour
    {
        private int _gold;

        public int Gold
        {
            get => _gold;
            private set => _gold = Mathf.Clamp(value, 0, int.MaxValue);
        }

        private readonly string _goldDropPoolName = "Gold";
        [SerializeField] private Transform goldTarget;
        
        public event Action OnGoldChanged;
        
        private void Start()
        {
            Gold = 0;
        }

        public void UseGold(int amount)
        {
            Gold -= amount;
            OnGoldChanged?.Invoke();
        }

        public void GetGold(int amount)
        {
            Gold += amount;
            OnGoldChanged?.Invoke();
        }
        
        public void SpawnGoldDrop(Vector3 spawnWorldPos, int totalGoldAmount, int split = 1)
        {
            if (PoolManager2.Instance == null)
            {
                Debug.LogWarning("PoolManager is null. Can not spawn GoldDrop.");
                GetGold(totalGoldAmount);
                return;
            }

            if (goldTarget == null)
            {
                Debug.LogWarning("Gold target is null. Can not move GoldDrop to UI.");
                GetGold(totalGoldAmount);
                return;
            }
            
            split = Mathf.Max(1, split);
            int amountPerDrop = Mathf.Max(1, totalGoldAmount / split);
            int rest = totalGoldAmount - amountPerDrop * split;
            
            Vector3 targetWorld = goldTarget.position;
            if (Camera.main != null && goldTarget is RectTransform)
            {
                targetWorld = goldTarget.position;
            }

            targetWorld.z = 0f;

            for (int i = 0; i < split; i++)
            {
                IPoolable poolItem = PoolManager2.Instance.Pop(_goldDropPoolName);

                if (poolItem == null)
                {
                    GetGold(amountPerDrop);
                    continue;
                }

                GameObject dropObj = poolItem.GameObject;
                dropObj.transform.position = spawnWorldPos;

                var drop = dropObj.GetComponent<GoldDrop>();
                if (drop == null)
                {
                    Debug.LogError("GoldDrop component not found in pooled object.");
                    GetGold(amountPerDrop);
                    continue;
                }
                
                int giveAmount = amountPerDrop + ((i == split - 1) ? rest : 0);

                drop.Setup(goldTarget, this, giveAmount);
            }
        }
    }
}