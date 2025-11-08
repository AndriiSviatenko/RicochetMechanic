using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Stack<T> _pool = new();

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        for (int i = 0; i < initialSize; i++)
        {
            _pool.Push(CreateNew());
        }
    }

    private T CreateNew()
    {
        var go = Object.Instantiate(_prefab.gameObject, _parent);
        go.SetActive(false);
        return go.GetComponent<T>();
    }

    public T Get()
    {
        if (_pool.Count == 0)
        {
            _pool.Push(CreateNew());
        }
        var item = _pool.Pop();
        item.gameObject.SetActive(true);
        return item;
    }
    public void Return(T item)
    {
        item.gameObject.SetActive(false);
        _pool.Push(item);
    }
}
