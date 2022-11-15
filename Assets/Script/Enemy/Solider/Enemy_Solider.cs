using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Solider : Enemy
{

    public Animator anim;
    public Rigidbody2D rb;
    public bool isHit;
    private Vector2 direction;
    public float speed;
    public AnimatorStateInfo info;
    public float hp;
    public SoliderData_SO soliderdata;
    public bool IsAttack;
 
 //   public Animator hitAnim;

   // public GameObject floatPoint;

    private EnemySoliderStats enemySoliderStats;

  

    protected override void Awake()
    {
        base.Start();
        base.Awake();
        anim = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        hp = soliderdata.maxhealth;
        enemySoliderStats = GetComponent<EnemySoliderStats>();

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

    public void GetHit(Vector2 direction)//用作外部调用，传入vector2用来设置击退方向
    {
        transform.localScale = new Vector3(direction.x, 1, 1);
        isHit = true;
        this.direction = direction;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
        {
            anim.Play("Hurt");
        }
    }

    public void TakeDamage(float damage)
    {
        //  floatPointBase(damage);      
        hp -= damage;
    }
    public void SkillDamage(float damage)
    {
       // floatPointBase(damage);
        hp -= damage;
    }
    public void Skill_longDamage(float damage)
    {
        floatPointBase(damage);
        hp -= damage;
    }



    public void Dead()
    {
        if (hp <= 0)
        {
            anim.Play("Dead");
            GetComponent<Collider2D>().enabled = false;
            rb.gravityScale = 0f;
        }
        //  lootSpawner.Spawn(transform.position);
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
