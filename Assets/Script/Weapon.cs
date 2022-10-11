using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject BulletPrefab;
    public float fireRate = 0.5F;//0.5��ʵ����һ���ӵ�
    private float nextFire = 0.0F;


    private void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        if (Time.time > nextFire)//���ӵ������м��
        {
            nextFire = Time.time + fireRate;//Time.time��ʾ����Ϸ���������ڵ�ʱ�䣬��������Ϸ����ͣ��ֹͣ���㡣
            Instantiate(BulletPrefab, transform.position, transform.rotation);
        }
    }
}