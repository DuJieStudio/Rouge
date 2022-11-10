using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerate : MonoBehaviour
{
    private Transform[] trf;//���������λ������
    public GameObject go;//����Ԥ���壬�Լ���ק
    public int enemyCount = 0;//��������

    void Start()
    {

        int count = this.transform.childCount;//�������������
        trf = new Transform[count];//ʵ��������,ΪʲôҪʵ��������Ϊ֮ǰֻ�Ǵ��������飬�������е�ֵΪnull
        for (int i = count - 1; i >= 0; i--)//�����˵�λ�ô������飬Ϊʲô����д���Կ�����һƪ����"Unity��Transform child out of bounds������"
        {
            trf[i] = transform.GetChild(i).transform;
        }

    }
    private float CreatTime = 1f;//��һ�δ������˵�ʱ��
    void Update()
    {

        CreatTime -= Time.deltaTime;    //��ʼ����ʱ

        if (CreatTime <= 0 && enemyCount < 4) //����ʱΪ0���ҵ�������С��4
        {
            GameObject go2 = Instantiate(go, trf[Random.Range(0, trf.Length)].position, Quaternion.identity);//�������ˣ����Ұ�������go2�������������
            CreatTime = 3;//��������ÿ3�����һ��
            enemyCount++;//����������1
            Destroy(go2, 4f);//����4�����ʧ
            enemyCount--;//������ʧ��������1
        }

    }
}
