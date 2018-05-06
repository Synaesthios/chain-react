using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool s_instance;

    public static ObjectPool Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
            }

            return s_instance;
        }
    }
    

    Dictionary<GameObject, Queue<GameObject>> m_objectPool = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject Acquire(GameObject prefab)
    {
        if (!m_objectPool.ContainsKey(prefab))
        {
            m_objectPool.Add(prefab, new Queue<GameObject>());
        }

        if (m_objectPool[prefab].Count == 0)
            return Instantiate(prefab);

        var obj = m_objectPool[prefab].Dequeue();
        obj.SetActive(true);
        obj.transform.SetParent(null);
        return obj;
    }

    public void Release(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        m_objectPool[prefab].Enqueue(obj);
    }
}
