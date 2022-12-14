using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flower : Enemy
{

    public GameObject bullet;
    public GameObject startPoint;

    public bool isHit;
    public AnimatorStateInfo info;
    private Vector2 direction;

    public SoliderData_SO FlowerData;
    public float hp;
    public Rigidbody2D rb;
    private EnemyFlowerStats enemyFlowerStats;
    public Animator anim;

    public Attack GetAttack;
    public float damage;


    protected override void Awake()
    {
        base.Start();
        base.Awake();

        gameObject.name = "Enemy_Flower";

        rb = gameObject.GetComponent<Rigidbody2D>();
        hp = FlowerData.maxhealth;
        enemyFlowerStats = GetComponent<EnemyFlowerStats>();
        anim = gameObject.GetComponent<Animator>();
        GetAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }


    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);//持续获取动画进度
        if (isHit)
        {          
            if (info.normalizedTime >= 0.6f)//动画播到一定进度后结束受击状态
            {
                isHit = false;
            }
        }

        Dead();

    }

    void ShotBullet()
    {

        Instantiate(bullet, startPoint.transform.position, transform.rotation);

    }

    public void GetHit()//用作外部调用，传入vector2用来设置击退方向
    {
       
        isHit = true;
        GetComponent<CreatHPBAR>().setHit(true);     
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
        {           
            anim.Play("Hurt");
        }
    }
    //public void floatPointBase(float damage)
    //{
    //    GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
    //    gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
    //}
    public void TakeDamage(float damage)
    {
        if (hp > 0)
        {
            floatPointBase(damage);
        //    damage = 1f * GetAttack.Power + UnityEngine.Random.Range(0, 4);
            hp -= damage;
        }
    }
    public void SkillDamage(float damage)
    {
        if (hp > 0)
        {
            floatPointBase(damage);
    //        damage = 1.5f * GetAttack.Power + UnityEngine.Random.Range(-3, 3);
            hp -= damage;
        }
    }
    public void SpecialDamage()
    {
        if (hp > 0)
        {
            floatPointBase(damage);
            damage = 1;
            hp -= damage;
        }
    }


    public void Dead()
    {
        if (hp <= 0)
        {
            anim.Play("Dead");
            GetComponent<Collider2D>().enabled = false;
            rb.gravityScale = 0f;
        }    
    }
}
