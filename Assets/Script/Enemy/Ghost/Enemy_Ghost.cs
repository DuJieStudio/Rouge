using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : MonoBehaviour
{

    public GameObject Ghost;
    public GameObject startPoint;
    public float generateTime =2f;
    public Animator anim;
    private Rigidbody2D rb;
    private int generatecount;
    public float damage;

    public bool isHit;
    public AnimatorStateInfo info;

    public float Hp_Ghost;
    public SoliderData_SO LightData;
    private EnemySoliderStats LightStats;
    public Attack GetAttack;


    void Start()
    {        
        anim = Ghost.GetComponent<Animator>();
        anim.Play("Appear");
        rb = Ghost.GetComponent<Rigidbody2D>();
        generatecount = 0;

        Hp_Ghost = LightData.maxhealth;
        LightStats = GetComponent<EnemySoliderStats>();
    }

    void Update()
    {
        generateTime -= Time.deltaTime;
        GenerateEnemy();

        info = anim.GetCurrentAnimatorStateInfo(0);//持续获取动画进度
        if (isHit)
        {
            // rb.velocity = direction * speed;
            if (info.normalizedTime >= 0.6f)//动画播到一定进度后结束受击状态
                isHit = false;
        }
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

    public void GitHit()
    {
        isHit = true;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
        {
            anim.Play("Light_Hurt");
        }
        Debug.Log("3212353452345");
    }

    public void TakeDamage()
    {
        //  floatPointBase(damage);  
        damage = 1f * GetAttack.Power + UnityEngine.Random.Range(0, 4);
        Hp_Ghost -= damage;
    }
    public void SkillDamage()
    {
        // floatPointBase(damage);
        damage = 1.5f * GetAttack.Power + UnityEngine.Random.Range(-3, 3);
        Hp_Ghost -= damage;
    }
}
