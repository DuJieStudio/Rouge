using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Solider : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;
    public bool isHit;
    private Vector2 direction;
    public float speed;
    public AnimatorStateInfo info;
    public float hp;
    public SoliderData_SO soliderdata;

    private EnemySoliderStats enemySoliderStats;

    private void Awake()
    {
        enemySoliderStats = GetComponent<EnemySoliderStats>();
    }

    void Start()
    {
        anim = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        hp = soliderdata.maxhealth;
    }

    
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);//持续获取动画进度
        if (isHit)
        {
           // rb.velocity = direction * speed;
            if (info.normalizedTime >= 0.6f)
                isHit = false;
        }

        if (hp <= 0)
        {
            anim.Play("Dead");

    }
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

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
