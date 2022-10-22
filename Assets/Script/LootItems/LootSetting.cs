using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public class LootSetting 
{
    public GameObject prefab;//战利品预制体
    [Range(0f,100f)]public float dropPercentage;//爆率（0到100）

    public void Spawn(Vector3 position)//接受三维向量来传递生成位置
    {
        if (Random.Range(0f, 100f) <= dropPercentage)
        {
            PoolManager.Release(prefab, position);
        }
    }
}
