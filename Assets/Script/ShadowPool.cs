using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;
    public GameObject ShadowPrefab;
    public int shadowCount;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    void Awake()
    {
        instance = this;

        //初始化对象池
        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(ShadowPrefab);
            newShadow.transform.SetParent(transform);

            //返回对象池,取消启用
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }


    public GameObject GetFormPool()
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();

        outShadow.SetActive(true);

        return outShadow;
    }
}
