using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ItemDrop : MonoBehaviour
{
  //  public static ItemDrop instance;

    public GameObject[] gos;

    //private void Awake()
    //{
    //    instance = this;
    //}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void fallingEquitment()
    public void fallingEquitment(Vector3 vector3)
    {
        //Vector3 pos = transform.position;//获取敌人位置也是掉落位置
        //Instantiate(gos[Random.Range(0, gos.Length)], pos, Quaternion.identity);


        //Instantiate(gos[Random.Range(0, gos.Length)], pos, Quaternion.identity);
        //Debug.Log("2222222");

    }
}
