using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyObject { Solider, Flower, Ghost,light}

public class Enemy_TakeDamage : MonoBehaviour
{

    public EnemyObject enemyType;
    public Attack GetAttack;
    public bool isAttackLong = false;

    public float useTime = 0;

    //protected override void Start()
    //{
    //    base.Start();
    //}
    private void Awake()
    {
        GetAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(GetAttack.comboStep);
        if (collision.gameObject.CompareTag("playerAttack"))
        {
            switch (enemyType)
            {
                case EnemyObject.Solider:
                    Solider_TakeDamage();
                    break;

                case EnemyObject.Flower:
                    Flower_TakeDamage();
                    break;
                case EnemyObject.Ghost:
                    Ghost_TakeDamage();
                    break;
                case EnemyObject.light:
                    Light_TakeDamage();
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
    public void Solider_TakeDamage()
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

    public void Ghost_TakeDamage()
    {
        if (transform.localScale.x > 0)
        {
            GetComponent<Ghost>().GetHit(Vector2.right);
        }
        else if (transform.localScale.x < 0)
        {
            GetComponent<Ghost>().GetHit(Vector2.left);
        }

        if (GetAttack.comboStep > 0)
        {
            GetComponent<Ghost>().TakeDamage(GetAttack.Damage);
        }
        else if (GetAttack.comboStep == 0)
        {
            if (GetAttack.isSkillShort)
            {
                GetComponent<Ghost>().SkillDamage(GetAttack.skillDamage);
            }
            else if (GetAttack.isSkillLong)
            {
                InvokeRepeating("SustainDamage", 0f, 0.5f);
            }
        }

    }

    public void Light_TakeDamage()
    {
        if (transform.localScale.x > 0)
        {
            GetComponent<Enemy_Light>().GetHit(Vector2.right);
        }
        else if (transform.localScale.x < 0)
        {
            GetComponent <Enemy_Light>().GetHit(Vector2.left);
        }

        if (GetAttack.comboStep > 0)
        {
            GetComponent<Enemy_Light>().TakeDamage(GetAttack.Damage);
        }
        else if (GetAttack.comboStep == 0)
        {
            if (GetAttack.isSkillShort)
            {
                GetComponent < Enemy_Light>().SkillDamage(GetAttack.skillDamage);
            }
            else if (GetAttack.isSkillLong)
            {
                InvokeRepeating("SustainDamage", 0f, 0.5f);
            }
        }
    }
}

