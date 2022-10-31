using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    public GameObject floatPoint;

    LootSpawner lootSpawner;
    protected virtual void Awake()
    {
        lootSpawner = GetComponent<LootSpawner>();     
    }

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void floatPointBase(float damage)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
    }


    public void Death()
    {

       // GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
        lootSpawner.Spawn(transform.position);
        // GameObject.Find("solider_attack1").SendMessage("fallingEquitment");
        //GameObject.Find("solider_attack1").GetComponent<ItemDrop>().fallingEquitment();     
        //ItemDrop.instance.fallingEquitment(this.transform.position);
    }
   
}
