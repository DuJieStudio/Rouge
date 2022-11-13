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

    public FlowerData_SO flowerData;
    public float hp;
    public Rigidbody2D rb;
    private EnemyFlowerStats enemyFlowerStats;
    public Animator anim;


    protected override void Awake()
    {
        base.Start();
        base.Awake();

        gameObject.name = "Enemy_Flower";

        rb = transform.GetComponent<Rigidbody2D>();
        hp = flowerData.maxhealth;
        enemyFlowerStats = GetComponent<EnemyFlowerStats>();
        anim = transform.GetComponent<Animator>();
    }


    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);//������ȡ��������
        if (isHit)
        {
            // rb.velocity = direction * speed;
            if (info.normalizedTime >= 0.6f)//��������һ�����Ⱥ�����ܻ�״̬
                isHit = false;
        }

        Dead();

    }

    void ShotBullet()
    {

        Instantiate(bullet, startPoint.transform.position, transform.rotation);

    }

    public void GetHit(Vector2 direction)//�����ⲿ���ã�����vector2�������û��˷���
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
        floatPointBase(damage);
        hp -= damage;
    }
    public void SkillDamage(float damage)
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
}
