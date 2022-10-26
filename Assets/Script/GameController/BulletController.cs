using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;//�ӵ����ٶ�
    public Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();//��ȡ�ӵ��������
        rig.velocity = new Vector2(-1,0) * speed;//�ƶ�
        Destroy(gameObject, 3);//3��������ӵ�����Ȼ�ӵ������޶�
    }

    private void OnTriggerEnter2D(Collider2D collision)//�����������ײ����ʱ��
    {
        //if (collision.gameObject.tag == "Player")//�����ײ�����ǵ���
        //{
        //    collision.gameObject.GetComponent<player>().Hurt();//���õ��˵����˺������¼��뵽���˵����溯�������۵���Ѫ��������鿴Ч������Ȼ̫����
        //}
        Destroy(gameObject);//ֻҪ��ײ����ײ��ʹݻ��ӵ�����
    }
}
