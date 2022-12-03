using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool isHit;
    public Vector2 direction;
    public GameObject thisghost;
    public Animator anim;
    public SoliderData_SO ghostdata;
    public float hp;
    public AnimatorStateInfo info;
    public float damage;
    public Attack GetAttack;

    private void Awake()
    {
        thisghost = this.gameObject;
        anim = gameObject.GetComponent<Animator>();
        ghostdata = GetComponent<EnemySoliderStats>().SoliderData;
        hp = ghostdata.maxhealth;
        GetAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();

    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
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
    public void GetHit()//用作外部调用，传入vector2用来设置击退方向
    {
        if (hp > 0)
        {
            isHit = true;
            GetComponent<CreatHPBAR>().setHit(true);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
            {
                anim.Play("Hurt");
            }
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
            //floatPointBase(damage);
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
            anim.Play("Dead");
            GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.tag = "Untagged";
            Destroy(thisghost, 0.5f);
        }
        //  lootSpawner.Spawn(transform.position);
    }
}
