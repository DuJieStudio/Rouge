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

    private void Awake()
    {
        thisghost = this.gameObject;
        anim = gameObject.GetComponent<Animator>();
        ghostdata = GetComponent<EnemySoliderStats>().SoliderData;
        hp = ghostdata.maxhealth;
        

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
        hp -= damage;
    }
    public void SkillDamage(float damage)
    {
        //   floatPointBase(damage);
        hp -= damage;
 
    }
    public void Dead()
    {
        if (hp <= 0)
        {
            anim.Play("Dead");
            Destroy(thisghost, 0.5f);
        }
        //  lootSpawner.Spawn(transform.position);
    }
}
