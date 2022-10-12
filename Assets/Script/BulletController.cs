using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;//子弹的速度
    public Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();//获取子弹刚体组件
        rig.velocity = new Vector2(-1,0) * speed;//移动
        Destroy(gameObject, 3);//3秒后销毁子弹，不然子弹会无限多
    }

    private void OnTriggerEnter2D(Collider2D collision)//触碰到别的碰撞器的时候
    {
        //if (collision.gameObject.tag == "Player")//如果碰撞对象是敌人
        //{
        //    collision.gameObject.GetComponent<player>().Hurt();//调用敌人的受伤函数，新加入到敌人的里面函数用来扣敌人血量，方便查看效果，不然太快了
        //}
        Destroy(gameObject);//只要碰撞到碰撞体就摧毁子弹本身
    }
}
