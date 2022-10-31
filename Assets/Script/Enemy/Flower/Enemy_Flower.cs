using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flower : MonoBehaviour
{

    public GameObject bullet;
    public GameObject startPoint;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void ShotBullet()
    {
        //Instantiate(bullet, startPoint.transform);
        Instantiate(bullet, startPoint.transform.position, transform.rotation);

    }

}
