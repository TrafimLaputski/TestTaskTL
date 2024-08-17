using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab = null;
    [SerializeField] private int _minItemsCount = 10;
    [SerializeField] private int _maxItemsCount = 100;
    [SerializeField] private bool _notLimit = false;

    protected List<T> _items = new List<T>();

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < _minItemsCount; i++)
        {
            T temp = CreateObject();
            _items.Add(temp);
        }
    }

    private T CreateObject()
    {
        T temp = Instantiate(_prefab, transform);
        temp.gameObject.SetActive(false);
        ConstructObject(temp);
        return temp;
    }

    public T GetItem()
    {
        foreach (T item in _items)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }

        if (_notLimit || _items.Count < _maxItemsCount)
        {
            T temp = CreateObject();
            _items.Add(temp);
            return temp;
        }

        return null;
    }

    public void Return(T item)
    {
        item.gameObject.SetActive(false);
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
    }

    protected abstract void ConstructObject(T obj);
}