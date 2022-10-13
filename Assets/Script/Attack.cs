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
    private int comboStep;
    public float interval = 2f;
    private float timer;


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

            isAttack = true;
            comboStep++;
            if (comboStep > 2)
                comboStep = 1;

            timer = interval;
            anim.SetTrigger("attack");
            anim.SetInteger("comboStep", comboStep);
            float facedirection = Input.GetAxisRaw("Horizontal");
            if (facedirection != 0)
            {
                rb.velocity = new Vector2(rb.transform.localScale.x * attackSpeed, rb.velocity.y);
            }
        }

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                comboStep = 0;
            }
        }
    }
   
    public void AttackOver()
    {
        isAttack = false;
    }
}
