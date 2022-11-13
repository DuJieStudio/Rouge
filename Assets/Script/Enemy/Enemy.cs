using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public Attack GetAttack;

    public GameObject floatPoint;




    LootSpawner lootSpawner;
    protected virtual void Awake()
    {
        lootSpawner = GetComponent<LootSpawner>();     
    }

    protected virtual void Start()
    {      

    }

    private void Update()
    {

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
     
    }



    //public void Solide_TakeDamage()
    //{
    //    Debug.Log("11111111111111");
    //    Debug.Log(GetAttack.comboStep);
    //    if (transform.localScale.x > 0)
    //    {
    //        GetComponent<Enemy_Solider>().GetHit(Vector2.right);
    //    }
    //    else if (transform.localScale.x < 0)
    //    {
    //        GetComponent<Enemy_Solider>().GetHit(Vector2.left);
    //    }

    //    if (GetAttack.comboStep > 0)
    //    {
    //        GetComponent<Enemy_Solider>().TakeDamage(GetAttack.Damage);
    //    }
    //    else if (GetAttack.comboStep == 0)
    //    {
    //        GetComponent<Enemy_Solider>().SkillDamage(GetAttack.skillDamage);
    //    }
    //}

    //public void Flower_TakeDamage()
    //{
    //    if (transform.localScale.x > 0)
    //    {
    //        GetComponent<Enemy_Flower>().GetHit(Vector2.right);
    //    }
    //    else if (transform.localScale.x < 0)
    //    {
    //        GetComponent<Enemy_Flower>().GetHit(Vector2.left);
    //    }

    //    if (GetAttack.comboStep > 0)
    //    {
    //        GetComponent<Enemy_Flower>().TakeDamage(GetAttack.Damage);
    //    }
    //    else if (GetAttack.comboStep == 0)
    //    {
    //        GetComponent<Enemy_Flower>().SkillDamage(GetAttack.skillDamage);
    //    }
    //}
}
