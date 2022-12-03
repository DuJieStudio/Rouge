using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{

    public GameObject Ghost;
    public GameObject startPoint;
    public float generateTime =2f;
    public Animator anim;
    private Rigidbody2D rb;
    private int generatecount;
    public bool isHit;
    public Vector2 direction;
    public GameObject thislight;
    public SoliderData_SO lightdata;
    public float hp;
    public AnimatorStateInfo info;

    public Attack GetAttack;
    public float damage;

    void Start()
    {    

        thislight = this.gameObject;     
        anim = thislight.GetComponent<Animator>();
        anim.Play("Appear");
        rb = Ghost.GetComponent<Rigidbody2D>();
        generatecount = 0;
        lightdata = GetComponent<EnemySoliderStats>().SoliderData;
        hp = lightdata.maxhealth;
        GetAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();

    }

    void Update()
    {

        generateTime -= Time.deltaTime;
        GenerateEnemy();
        info = anim.GetCurrentAnimatorStateInfo(0);
        if (isHit)
        {
            if (info.normalizedTime >= 0.6f)
            {
                isHit = false;
            }
        }
        Dead();
    }
    void GenerateEnemy()
    {

        if (generatecount < 3)
        {
            if (generateTime <= 0)
            {
                Instantiate(Ghost, startPoint.transform.position, transform.rotation);
                generateTime = 2;

            }
        }
        if (generateTime == 2)
        {
            generatecount += 1;
        }

             
    }
    public void GetHit()//用作外部调用，传入vector2用来设置击退方向
    {
        if (hp > 0)
        {
            isHit = true;
            GetComponent<CreatHPBAR>().setHit(true);
            anim.Play("Light_Hurt");
        }
    }
    public void TakeDamage()
    {
        if (hp > 0)
        {
            damage = 1f * GetAttack.Power + UnityEngine.Random.Range(0, 4);
            hp -= damage;
        }
    }
    public void SkillDamage()
    {
        if (hp > 0)
        {
            //   floatPointBase(damage);
            damage = 1.5f * GetAttack.Power + UnityEngine.Random.Range(-3, 3);
            hp -= damage;
        }
    }
    public void SpecialDamage()
    {
        if (hp > 0)
        {
            //floatPointBase(damage);
            damage = 1;
            hp -= damage;
        }
    }


    public void Dead()
    {

        if (hp <= 0)
        {
            anim.Play("Light_Dead");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light_Dead") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
            {
                anim.enabled = false;
                this.enabled = false;
            }

        }
        //  lootSpawner.Spawn(transform.position);
    }
}
