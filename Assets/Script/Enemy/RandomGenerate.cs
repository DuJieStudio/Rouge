using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerate : MonoBehaviour
{
    private Transform[] trf;//创建随机点位置数组
    public GameObject go;//敌人预制体，自己拖拽
    public int enemyCount = 0;//敌人数量

    void Start()
    {

        int count = this.transform.childCount;//获得随机点的数量
        trf = new Transform[count];//实例化数组,为什么要实例化，因为之前只是创建了数组，但数组中的值为null
        for (int i = count - 1; i >= 0; i--)//将敌人的位置存入数组，为什么这样写可以看我另一篇文章"Unity中Transform child out of bounds的问题"
        {
            trf[i] = transform.GetChild(i).transform;
        }

    }
    private float CreatTime = 1f;//第一次创建敌人的时间
    void Update()
    {

        CreatTime -= Time.deltaTime;    //开始倒计时

        if (CreatTime <= 0 && enemyCount < 4) //倒计时为0并且敌人数量小于4
        {
            GameObject go2 = Instantiate(go, trf[Random.Range(0, trf.Length)].position, Quaternion.identity);//创建敌人，并且把它赋给go2，方便后续管理
            CreatTime = 3;//后续敌人每3秒出来一次
            enemyCount++;//敌人数量加1
            Destroy(go2, 4f);//存在4秒后消失
            enemyCount--;//敌人消失后，数量减1
        }

    }
}
