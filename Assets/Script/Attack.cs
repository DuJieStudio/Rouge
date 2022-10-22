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
    public float interval;
    private float timer;
    public float damage;

    private PlayerAttackStats playerAttackStats;

    private void Awake()
    {
        playerAttackStats = GetComponent<PlayerAttackStats>();
    }


    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
        damage = playerAttackStats.MinDamage;
    }

    
    void Update()
    {
        PlayerAttack();

    }

    void PlayerAttack()
    {

        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            //SoundManager.instance.Attack2Audio();

            isAttack = true;
            comboStep++;
            if (comboStep > 3)
                comboStep = 1;
            //if (comboStep == 3)
            //    SoundManager.instance.Attack1Audio();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            
            if (transform.localScale.x > 0)
                collision.GetComponent<Enemy_Solider>().GetHit(Vector2.right);
            else if (transform.localScale.x < 0)
                collision.GetComponent<Enemy_Solider>().GetHit(Vector2.left);


            collision.GetComponent<Enemy_Solider>().TakeDamage(damage);
        }
    }


}
