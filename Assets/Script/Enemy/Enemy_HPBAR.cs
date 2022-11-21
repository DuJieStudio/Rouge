using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HPBAR : MonoBehaviour
{
    public GameObject thisgameobject;
    public SoliderData_SO enemydata;
    public Image HPBar_IMG;
    public float currenthp;  // Start is called before the first frame update
    void Start()
    {
        thisgameobject = this.transform.parent.transform.gameObject;
        enemydata = thisgameobject.GetComponent<EnemySoliderStats>().SoliderData;
        HPBar_IMG = GetComponent<Canvas>().GetComponentInChildren<Slider>().GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HPBar_IMG.fillAmount = (currenthp / enemydata.maxhealth);
        getdata();
    }
    void getdata()
    { 
        if (thisgameobject.GetComponent<Enemy_TakeDamage>().enemyType==EnemyObject.light)
        { currenthp = thisgameobject.GetComponent<Enemy_Light>().hp; }
        if (thisgameobject.GetComponent<Enemy_TakeDamage>().enemyType == EnemyObject.Flower)
        { currenthp = thisgameobject.GetComponent<Enemy_Flower>().hp; }
        if (thisgameobject.GetComponent<Enemy_TakeDamage>().enemyType == EnemyObject.Solider)
        { currenthp = thisgameobject.GetComponent<Enemy_Solider>().hp; }
        if (thisgameobject.GetComponent<Enemy_TakeDamage>().enemyType == EnemyObject.Ghost)
        { currenthp = thisgameobject.GetComponent<Ghost>().hp;}
        if (currenthp <= 0)
        { Destroy(this.gameObject); }






    }

}
