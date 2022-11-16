using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyObject { Solider, Flower, Ghost, Light }

public class Enemy_TakeDamage : MonoBehaviour
{

    public EnemyObject enemyType;
    public Attack GetAttack;
    public bool isAttackLong = false;  
    public float useTime=0;



    //public SoliderData_SO LightData;
    //private EnemySoliderStats LightStats;
    //public float Hp_Light;

    //protected override void Start()
    //{
    //    base.Start();
    //}
    void Awake()
    {
        //LightStats = GetComponent<EnemySoliderStats>();
        //Hp_Light = LightData.maxhealth;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(GetAttack.comboStep);
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

                case EnemyObject.Light:
                    Ghost_TakeDamage();
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
            //      GetComponent<Enemy_Solider>().TakeDamage(GetAttack.Power);     
            GetComponent<Enemy_Solider>().TakeDamage();
        }
        else if (GetAttack.comboStep == 0)
        {
            if (GetAttack.isSkillShort)
            {
                GetComponent<Enemy_Solider>().SkillDamage();
            }
            else if (GetAttack.isSkillLong)
            {
                InvokeRepeating("SustainDamage", 0f, 0.5f);
            }
        }
    }

    //void SustainDamage()
    //{
    //    switch (enemyType)
    //    {
    //        case EnemyObject.Solider:
    //            GetComponent<Enemy_Solider>().SkillDamage();
    //            break;

    //        case EnemyObject.Flower:
    //            GetComponent<Enemy_Flower>().SkillDamage();
    //            break;
    //    }       
    //    useTime += 1;
    //    if (useTime == 5)
    //    {
    //        useTime = 0;
    //        this.CancelInvoke();
    //    }
    //}

    public void Flower_TakeDamage()
    {
        Debug.Log("123123123");
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
            GetComponent<Enemy_Flower>().TakeDamage();
        }
        else if (GetAttack.comboStep == 0)
        {           
            if (GetAttack.isSkillShort)
            {
                GetComponent<Enemy_Flower>().SkillDamage();
            }
            else if (GetAttack.isSkillLong)
            {
                InvokeRepeating("SustainDamage", 0f, 0.5f);
            }
        }
    }

    public void Ghost_TakeDamage()
    {
        GetComponent<Enemy_Ghost>().GitHit();

        if (GetAttack.comboStep > 0)
        {
            GetComponent<Enemy_Ghost>().TakeDamage();
        }
        else if (GetAttack.comboStep == 0)
        {
            if (GetAttack.isSkillShort)
            {
                GetComponent<Enemy_Ghost>().SkillDamage();
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
                GetComponent<Enemy_Solider>().SkillDamage();
                break;

            case EnemyObject.Flower:
                GetComponent<Enemy_Flower>().SkillDamage();
                break;
        }
        useTime += 1;
        if (useTime == 5)
        {
            useTime = 0;
            this.CancelInvoke();
        }
    }
}
