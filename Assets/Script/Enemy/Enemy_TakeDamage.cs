using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyObject { Solider, Flower, Ghost }

public class Enemy_TakeDamage : Attack
{

    public EnemyObject enemyType;
    public Attack GetAttack;
  //  public bool isattack;
   // public  Animator anim;
 


    //protected override void Start()
    //{
    //    base.Start();
    //}

    void Update()
    {
    //    isattack = GetComponent<Enemy_Solider>().IsAttack;
     
    //    anim = GetComponent<Enemy_Solider>().anim;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerAttack"))
        {
            switch (enemyType)
            {
                case EnemyObject.Solider:
                    Solide_TakeDamage();
                    break;

                case EnemyObject.Flower:
                    Flower_TakeDamage();
                    break;
            }
        }
        //else if (collision.gameObject.CompareTag("playerBlock"))
        //{
        //    switch (enemyType)
        //    {
        //        case EnemyType.Solider:
        //      //      if (isattack)
        //     //       {
        //     //           anim.SetTrigger("Yun");
        //        //        anim.SetBool("Attack", false);
        //        //    }
        //            break;
        //    }
        //}
    }
    public void Solide_TakeDamage()
    {
        Debug.Log("11111111111111");
        Debug.Log(GetAttack.comboStep);
        if (transform.localScale.x > 0)
        {
            GetComponent<Enemy_Solider>().GetHit(Vector2.right);
        }
        else if (transform.localScale.x < 0)
        {
            GetComponent<Enemy_Solider>().GetHit(Vector2.left);
        }

        if (GetAttack.comboStep > 0)
        {
            GetComponent<Enemy_Solider>().TakeDamage(GetAttack.Damage);
        }
        else if (GetAttack.comboStep == 0)
        {
            GetComponent<Enemy_Solider>().SkillDamage(GetAttack.skillDamage);
        }
    }

    public void Flower_TakeDamage()
    {
        if (transform.localScale.x > 0)
        {
            GetComponent<Enemy_Flower>().GetHit(Vector2.right);
        }
        else if (transform.localScale.x < 0)
        {
            GetComponent<Enemy_Flower>().GetHit(Vector2.left);
        }

        if (GetAttack.comboStep > 0)
        {
            GetComponent<Enemy_Flower>().TakeDamage(GetAttack.Damage);
        }
        else if (GetAttack.comboStep == 0)
        {
            GetComponent<Enemy_Flower>().SkillDamage(GetAttack.skillDamage);
        }
    }
}
