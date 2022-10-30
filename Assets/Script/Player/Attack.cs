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

    [Header("普攻相关")]
    public float attackSpeed;
    public bool isAttack;
    private int comboStep;
    public float interval;
    private float timer;
    public float Damage;
   

    [Header("技能相关")]
    public float skillDamage;
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
        Damage = playerAttackStats.MinDamage;
        skillDamage = playerAttackStats.SkillDamage;
        isGather = false;
      //  skillReady = false;
        
    }


    void Update()
    {

        PlayerAttack();
        gathering();
        chargeing();
        skillAttack();

    }

    void PlayerAttack()
    {

        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            //SoundManager.instance.Attack2Audio();

            isAttack = true;
        //    isGather = true;
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
        if (Input.GetKeyDown(KeyCode.K)&& skillReady&&!isAttack)
 
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

        if (!isGather && gatherTime > 1f && !isSkill  )
        {
            gatherTime = 0;
            comboStep = 0;
            isSkill = true;
            anim.SetTrigger("skill2");        
        }
        else if (!isGather && gatherTime > 0 && gatherTime <= 1f && !isSkill)
        {
            gatherTime = 0;
            comboStep = 0;
            anim.SetTrigger("skill1");
            isSkill = true;           
           
        }

    }

    void chargeing()
    {
        if (gatherTime > 0 && gatherTime <= 1f)
        {
            anim.Play("charge");
        }
        else if (gatherTime > 1f && gatherTime <= 4f)
        {
            anim.Play("charge2");
        }
        else if(gatherTime>4f)
        {
            isGather = false;
            isAttack = false;
            isSkill = false;
            gatherTime = 0;
            anim.SetTrigger("skill2");
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
       // isGather = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {            
                if (transform.localScale.x > 0)
                    collision.GetComponent<Enemy_Solider>().GetHit(Vector2.right);
                else if (transform.localScale.x < 0)
                    collision.GetComponent<Enemy_Solider>().GetHit(Vector2.left);

        
            if (comboStep > 0)
            {
                collision.GetComponent<Enemy_Solider>().TakeDamage(Damage);             

            }
            else if (comboStep == 0)
            {
                collision.GetComponent<Enemy_Solider>().SkillDamage(skillDamage);
            }
           
        }
    }


}
