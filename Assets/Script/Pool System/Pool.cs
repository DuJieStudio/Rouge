using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Pool
{
    public GameObject Prefab
    {
        get { return prefab; }
    }

    public int Size => size;
    public int RuntimeSize => queue.Count;
    [SerializeField]GameObject prefab;
    Queue<GameObject> queue;
    [SerializeField]int size = 1;
    Transform parent;


    public  void InitiaLize(Transform parent)//初始化工作,因为要在其他函数中调用，故用public
    {
        queue = new Queue<GameObject>();//队列初始化
        this.parent = parent;
        for (var i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()//因为所有对象都是prefab预制体，函数返回值是一个游戏对象类型，所以用gameobject 
    {
        var copy = GameObject.Instantiate(prefab,parent);//生成一个prefab的复制体，让其处于随时可被启用并投入工作状态,用copy存储起来

        copy.SetActive(false);

        return copy;
    }

    GameObject AvailableObject()
    {
        GameObject availableObject = null;
        if (queue.Count > 0 && !queue.Peek().activeSelf)//当队列元素数量大于0,并且队列第一个元素不是在启用状态，调用dequeue函数
        {
            availableObject = queue.Dequeue();
        }
        else
        {
            availableObject = Copy();
        }

        queue.Enqueue(availableObject);//对象出列后让其入列达到返回对象池的效果

        return availableObject;
    }

    public  GameObject preparedObject()
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);

        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position)//重载，怎加位置参数
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;

        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position,Quaternion rotation)//重载，增加旋转参数
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;

        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position, Quaternion rotation,Vector3 localScale)//重载,增加一个三维向量的缩放值参数
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;

        return preparedObject;
    }
    //public void Return(GameObject gameObject)//返回对象池
    //{
    //    queue.Enqueue(gameObject);
    //}
}
