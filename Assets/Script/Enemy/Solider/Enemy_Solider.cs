using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Solider : Enemy
{

    public Animator anim;
    public Animator anim_attack;
    public Rigidbody2D rb;
    public bool isHit;
    private Vector2 direction;
    public float speed;
    public AnimatorStateInfo info;
    public float hp;
    public SoliderData_SO soliderdata;
    public bool IsAttack;

    public Attack GetAttack;
 //   public float damage;//收到的伤害
 //   public Animator hitAnim;

   // public GameObject floatPoint;

    private EnemySoliderStats enemySoliderStats;

  

    protected override void Awake()
    {
        base.Start();
        base.Awake();
        anim = transform.GetComponent<Animator>();
        anim_attack = transform.GetChild(1).GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        hp = soliderdata.maxhealth;
        enemySoliderStats = GetComponent<EnemySoliderStats>();
        GetAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }


    void Update()
    {
       
         // Debug.Log(this.transform.position);
         info = anim.GetCurrentAnimatorStateInfo(0);//持续获取动画进度
        if (isHit)
        {
            // rb.velocity = direction * speed;
            if (info.normalizedTime >= 0.6f)//动画播到一定进度后结束受击状态
                isHit = false;
        }
      
        Dead();
        //    Debug.Log(anim.GetBool("Hurt"));
    }

    public void GetHit()
    {
        if (hp > 0)
        {
            isHit = true;
            GetComponent<CreatHPBAR>().setHit(true);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
            {
                anim.Play("Hurt");
                anim_attack.Play("solider_effect");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (hp > 0)
        {
            floatPointBase(damage);
          //  damage = 1f * GetAttack.Power + UnityEngine.Random.Range(0, 4);
            hp -= damage;
        }
    }

    public void SkillDamage(float damage)
    {
        if (hp > 0)
        {
            floatPointBase(damage);
        //    damage = 1.5f * GetAttack.Power + UnityEngine.Random.Range(-3, 3);
            hp -= damage;
        }
    }
    //public void floatPointBase(float damage)
    //{
    //    GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
    //    gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
    //}

    public void SpecialDamage(float damage)
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
            this.gameObject.tag = "Untagged";
            rb.gravityScale = 0f;
        }
    }  

    public void AttackMove()
    {
        rb.velocity = new Vector2(rb.transform.localScale.x * -2.5f, rb.velocity.y);
    }


    void AttackStart()
    {
        IsAttack = true;
    }
    void AttackOver()
    {
        IsAttack = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerBlock"))
        {
            if (IsAttack)
            {
                anim.SetTrigger("Yun");
                anim.SetBool("Attack", false);
                //anim.SetTrigger("Yun");
                // anim.SetBool("Attack", false);
            }
        }
    }
}
