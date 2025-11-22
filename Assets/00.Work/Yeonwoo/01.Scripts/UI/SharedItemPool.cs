using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public static class SharedItemPool
    {
        private class PoolData
        {
            public Stack<GameObject> Stack = new Stack<GameObject>();
            public List<GameObject> List = new List<GameObject>();
#if UNITY_EDITOR
            public HashSet<int> SeenEditorIds = new HashSet<int>();
#endif
            public HashSet<string> SeenRuntimeNames = new HashSet<string>();
        }

        private static Dictionary<string, PoolData> _pools = new Dictionary<string, PoolData>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetAll()
        {
            _pools = new Dictionary<string, PoolData>();
        }

        public static void AddItems(IEnumerable<GameObject> items, string poolName)
        {
            if (items == null) return;
            if (!_pools.TryGetValue(poolName, out var pd))
            {
                pd = new PoolData();
                _pools[poolName] = pd;
            }

            var newlyAdded = new List<GameObject>();

            foreach (var it in items)
            {
                if (it == null) continue;

#if UNITY_EDITOR
                Object source = PrefabUtility.GetCorrespondingObjectFromSource(it) ?? it;
                int key = source.GetInstanceID();
                if (pd.SeenEditorIds.Add(key))
                {
                    newlyAdded.Add(it);
                }
#else
                string key = it.name;
                if (pd.SeenRuntimeNames.Add(key))
                {
                    newlyAdded.Add(it);
                }
#endif
            }

            if (newlyAdded.Count == 0) return;

            pd.List.AddRange(newlyAdded);
            Shuffle(pd.List);
            pd.Stack = new Stack<GameObject>(pd.List);
        }

        public static GameObject PopNext(string poolName)
        {
            if (!_pools.TryGetValue(poolName, out var pd)) return null;
            if (pd.Stack == null || pd.Stack.Count == 0) return null;
            return pd.Stack.Pop();
        }

        public static int Count(string poolName)
        {
            if (!_pools.TryGetValue(poolName, out var pd)) return 0;
            return pd.Stack?.Count ?? 0;
        }

        private static void Shuffle(List<GameObject> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
