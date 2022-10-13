using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Animator anim;
    public Rigidbody2D rb;
    private PolygonCollider2D coll;
    //public float attackCoolDown;
    //public float attackCD = 0;
    public float attackSpeed;
    public bool isAttack;

    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
    }

    
    void Update()
    {
        PlayerAttack();

    }

    void PlayerAttack()
    {
     
        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            //anim.SetTrigger("attack");
            ////isAttack = true;
            //rb.velocity = new Vector2(rb.transform.localScale.x * attackSpeed, rb.velocity.y);

            isAttack = true;
            anim.SetTrigger("attack");
            float facedirection = Input.GetAxisRaw("Horizontal");
            if (facedirection != 0)
            {
                rb.velocity = new Vector2(rb.transform.localScale.x * attackSpeed, rb.velocity.y);
            }
        }
    }
   
    public void AttackOver()
    {
        isAttack = false;
    }
}
