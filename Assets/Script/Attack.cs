using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Animator anim;
    private Animator anim2;
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

    public bool isGather;
    public float gatherTime;
    public bool isSkill;
    public float skillCoolDown;
    public bool skillReady=true;

    private PlayerAttackStats playerAttackStats;

    private void Awake()
    {
        playerAttackStats = GetComponent<PlayerAttackStats>();
    }


    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        anim2 = GameObject.FindGameObjectWithTag("enemy").GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
        damage = playerAttackStats.MinDamage;
        isGather = false;
      //  skillReady = false;
        
    }


    void Update()
    {

        PlayerAttack();
        gathering();

        skillAttack();

    }

    void PlayerAttack()
    {

        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            //SoundManager.instance.Attack2Audio();

            isAttack = true;
            isSkill = true;
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

    void gathering()
    {
        if (Input.GetKeyDown(KeyCode.K)&& skillReady)
        {
            isGather = true;
            isAttack = true;
            skillReady = false;

        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            isGather = false;         
        }
    }

    void skillAttack()
    {
        if (isGather)
        {
            gatherTime += Time.deltaTime;
        }

        if (!isGather && gatherTime > 0.5f && !isSkill)
        {
            gatherTime = 0;
            comboStep = 0;
            isSkill = true;           

            Debug.Log("动画没做重开吧");
        }
        else if (!isGather && gatherTime > 0 && gatherTime <= 0.5f && !isSkill)
        {
            gatherTime = 0;
            comboStep = 0;
            anim.SetTrigger("skill1");
            isSkill = true;           
           
        }

    }

    IEnumerator SkillCoolDownTime()
    {
        yield return new WaitForSeconds(skillCoolDown);

        skillReady = true;
       // Debug.Log("11111");
    }


    public void SkillBeginCoolDown()
    {
        StartCoroutine(SkillCoolDownTime());
    }


    public void SkillOver()
    {
        isSkill = false;
        isAttack = false;
    }
   
    public void AttackOver()
    {
        isAttack = false;
        isSkill = false;

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
