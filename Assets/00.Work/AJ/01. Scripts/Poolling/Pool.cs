using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Stack<IPoolable> _pool;
    private Transform _parent;
    private IPoolable _poolable;
    private GameObject _prefab;

    public Pool(IPoolable poolable, Transform parent, int initCount)
    {
        _pool = new Stack<IPoolable>(); // 풀을 쓰기 가장 좋은거
        _parent = parent;
        _poolable = poolable;
        _prefab = poolable.GameObject;

        for (int i = 0; i < initCount; i++)
        {
            GameObject item = Object.Instantiate(_prefab, _parent);
            item.name = _poolable.ItemName; 
            IPoolable poolableItem = item.GetComponent<IPoolable>(); // 아이템 이름을 초기화 시켜준다.
            _pool.Push(poolableItem);
        }
    }

    public IPoolable Pop()
    {
        IPoolable item = null;
        if (_pool.Count == 0)
        {
            GameObject gameObj = Object.Instantiate(_prefab, _parent);
            gameObj.name = _poolable.ItemName;
            item = gameObj.GetComponent<IPoolable>();
        }
        else
        {
            item = _pool.Pop();
            item.GameObject.SetActive(true); // 꺼져있던 아이템을 활성화 시켜준다
        }

        return item;
    }

    public void Push(IPoolable item)
    {
        item.GameObject.SetActive(false);
        _pool.Push(item);
    }
}
