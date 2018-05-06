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
                s_instance.transform.position = new Vector3(0, -1000, 0);
            }

            return s_instance;
        }
    }
    

    Dictionary<GameObject, Queue<GameObject>> m_objectPool = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject Acquire(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!m_objectPool.ContainsKey(prefab))
        {
            m_objectPool.Add(prefab, new Queue<GameObject>());
        }

        if (m_objectPool[prefab].Count == 0)
            return Instantiate(prefab, position, rotation);

        var obj = m_objectPool[prefab].Dequeue();
        obj.transform.SetParent(null);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public void Release(GameObject prefab, GameObject obj)
    {
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = Vector3.zero;
        //m_objectPool[prefab].Enqueue(obj);
    }
}
