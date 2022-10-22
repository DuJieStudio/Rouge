using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Solider : Enemy
{
    
   // public Animator anim;
    public Rigidbody2D rb;
    public bool isHit;
    private Vector2 direction;
    public float speed;
    public AnimatorStateInfo info;
    public float hp;
    public SoliderData_SO soliderdata;

    private EnemySoliderStats enemySoliderStats;

    //LootSpawner lootSpawner;
    //protected virtual void Awake()
    //{
    //    lootSpawner = GetComponent<LootSpawner>();

    //    enemySoliderStats = GetComponent<EnemySoliderStats>();
    //}


    private void Awake()
    {
        enemySoliderStats = GetComponent<EnemySoliderStats>();
    }

    protected override void Start()
    {
        base.Start();
        base.Awake();
       // anim = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        hp = soliderdata.maxhealth;
    }

    
    void Update()
    {
       // Debug.Log(this.transform.position);
        info = anim.GetCurrentAnimatorStateInfo(0);//持续获取动画进度
        if (isHit)
        {
           // rb.velocity = direction * speed;
            if (info.normalizedTime >= 0.6f)
                isHit = false;
        }

        Dead();
       
    }

    public void GetHit(Vector2 direction)//用作外部调用，传入vector2用来设置击退方向
    {
        transform.localScale = new Vector3(direction.x, 1, 1);
        isHit = true;
        this.direction = direction;
        anim.SetTrigger("Hit");
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    public void Dead()
    {
        if (hp <= 0)
        {
            anim.Play("Dead");
        }

      //  lootSpawner.Spawn(transform.position);
    }

    //public void Death()
    //{

    //    GetComponent<Collider2D>().enabled = false;
    //    Destroy(gameObject);
    //    // GameObject.Find("solider_attack1").SendMessage("fallingEquitment");
    //    //GameObject.Find("solider_attack1").GetComponent<ItemDrop>().fallingEquitment();     
    //    //ItemDrop.instance.fallingEquitment(this.transform.position);
    //}
}
