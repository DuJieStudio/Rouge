using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyObject { Solider, Flower, Ghost }

public class Enemy_TakeDamage : MonoBehaviour
{

    public EnemyObject enemyType;
    public Attack GetAttack;
    public bool isAttackLong = false;

    public float useTime=0;

    //protected override void Start()
    //{
    //    base.Start();
    //}

    void Update()
    {
        
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
            if (GetAttack.isSkillShort)
            {
                GetComponent<Enemy_Solider>().SkillDamage(GetAttack.skillDamage);
            }
            else if (GetAttack.isSkillLong)
            {           
                InvokeRepeating("SustainDamage", 0f, 0.5f);
            }
        }     
    }

    void SustainDamage()
    {
        switch (enemyType)
        {
            case EnemyObject.Solider:
                GetComponent<Enemy_Solider>().SkillDamage(GetAttack.skillDamage);
                break;

            case EnemyObject.Flower:
                GetComponent<Enemy_Flower>().SkillDamage(GetAttack.skillDamage);
                break;
        }
        // GetComponent<Enemy_Solider>().SkillDamage(GetAttack.skillDamage);
        useTime += 1;
        if (useTime == 5)
        {
            useTime = 0;
            this.CancelInvoke();
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
            if (GetAttack.isSkillShort)
            {
                GetComponent<Enemy_Flower>().SkillDamage(GetAttack.skillDamage);
            }
            else if (GetAttack.isSkillLong)
            {
                InvokeRepeating("SustainDamage", 0f, 0.5f);
            }
        }
    }

   
}
