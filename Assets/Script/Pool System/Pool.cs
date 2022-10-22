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


    public  void InitiaLize(Transform parent)//��ʼ������,��ΪҪ�����������е��ã�����public
    {
        queue = new Queue<GameObject>();//���г�ʼ��
        this.parent = parent;
        for (var i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()//��Ϊ���ж�����prefabԤ���壬��������ֵ��һ����Ϸ�������ͣ�������gameobject 
    {
        var copy = GameObject.Instantiate(prefab,parent);//����һ��prefab�ĸ����壬���䴦����ʱ�ɱ����ò�Ͷ�빤��״̬,��copy�洢����

        copy.SetActive(false);

        return copy;
    }

    GameObject AvailableObject()
    {
        GameObject availableObject = null;
        if (queue.Count > 0 && !queue.Peek().activeSelf)//������Ԫ����������0,���Ҷ��е�һ��Ԫ�ز���������״̬������dequeue����
        {
            availableObject = queue.Dequeue();
        }
        else
        {
            availableObject = Copy();
        }

        queue.Enqueue(availableObject);//������к��������дﵽ���ض���ص�Ч��

        return availableObject;
    }

    public  GameObject preparedObject()
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);

        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position)//���أ�����λ�ò���
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;

        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position,Quaternion rotation)//���أ�������ת����
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;

        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position, Quaternion rotation,Vector3 localScale)//����,����һ����ά����������ֵ����
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;

        return preparedObject;
    }
    //public void Return(GameObject gameObject)//���ض����
    //{
    //    queue.Enqueue(gameObject);
    //}
}
