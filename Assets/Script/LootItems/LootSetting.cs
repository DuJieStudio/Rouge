using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public class LootSetting 
{
    public GameObject prefab;//ս��ƷԤ����
    [Range(0f,100f)]public float dropPercentage;//���ʣ�0��100��

    public void Spawn(Vector3 position)//������ά��������������λ��
    {
        if (Random.Range(0f, 100f) <= dropPercentage)
        {
            PoolManager.Release(prefab, position);
        }
    }
}
