using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{

    public GameObject Specical;
    public GameObject SpecialStartPoint;
    private Animator anim;
    // private Animator anim2;
    public Rigidbody2D rb;
    private PolygonCollider2D coll;
    //public float attackCoolDown;
    //public float attackCD = 0;

    // public LayerMask IsEnemy;    
    public float ffTimer, ffTimerTotal;

    [Header("普攻相关")]
    public float damage;
    public float attackSpeed;
    public bool isAttack;
    public int comboStep;
    public float interval;
    private float timer;
    public float Power;

 
    [Header("技能相关")]
    public float skillDamage;
    public bool isGather;
    public float gatherTime;
    public bool isSkill;
    public float skillCoolDown;
    public bool skillReady = true;

    public bool isSkillShort;
    public bool isSkillLong;

    [Header("special相关")]
    public bool isSpecial;
    public bool SpecialReady;


    [Header("格挡相关")]
    public bool isBlock;

    public PlayerAttackStats playerAttackStats;

    public Cinemachine.CinemachineCollisionImpulseSource MyInpulse;
  //  public void GenerateImpulse(Vector3 velocity);
  //  public void GenerateImpulse(Vector3 position,Vector3 velocity);

    //  public RaycastHit2D[] hitInfo;

    // public Collider2D rayColl;

    private void Awake()
   // protected virtual void Awake()
    {
        playerAttackStats = GetComponent<PlayerAttackStats>();
    }


   // void Start()
    protected virtual void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        // anim2 = GameObject.FindGameObjectWithTag("enemy").GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
        Power = playerAttackStats.Power;
//        skillDamage = playerAttackStats.SkillDamage;
        isGather = false;
        isSpecial = false;
        //  skillReady = false;
        //Debug.Log(playerAttackStats.MinDamage);

        MyInpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
    }
    public void GenerateImpulse()
    {      
        Debug.Log("121212121");
    }
 
    //private void FixedUpdate()
    //{
    //    if (ffTimer > 0)
    //    {
    //        ffTimer -= Time.deltaTime;
    //        Time.timeScale = Mathf.Lerp(0.5f, 1f, (1 - (ffTimer / ffTimerTotal)));
    //    }
    //}

    void Update()
    {
        
        PlayerAttack();
        gathering();
        chargeing();
        skillAttack();
        Block();
        Special_Long();

       
        //    SimpleRays();
        //AttackCheck();
        
    }  

    void PlayerAttack()
    {

        if (Input.GetKeyDown(KeyCode.J) && !isAttack && !isBlock)
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

    void Block()
    {
        if (Input.GetKeyDown(KeyCode.H) && !isBlock && !isAttack )
        {
            anim.Play("block");
            isBlock = true;
        }
       
    }

    void gathering()
    {
        if (Input.GetKeyDown(KeyCode.K) && skillReady && !isAttack && !isBlock)

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

        if (!isGather && gatherTime > 1f && !isSkill && !isBlock )
        {
            gatherTime = 0;
            comboStep = 0;
            isSkill = true;
            anim.SetTrigger("skill2");
            isSkillLong = true;
        }
        else if (!isGather && gatherTime > 0 && gatherTime <= 1f && !isSkill && !isBlock )
        {
            gatherTime = 0;
            comboStep = 0;
            anim.SetTrigger("skill1");
            isSkill = true;
            isSkillShort = true;
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
        else if (gatherTime > 4f)
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
    }


    public void SkillBeginCoolDown()
    {
        StartCoroutine(SkillCoolDownTime());
    }


    public void SkillOver()
    {
        isSkill = false;
        isAttack = false;
        if (isSkillLong)
            isSkillLong = false;
        else if (isSkillShort)
            isSkillShort = false;
        //SkillBeginCoolDown();
    }

    public void AttackOver()
    {
        isAttack = false;
        isSkill = false;
        // isGather = false;
    }
    public void BlockOver()
    {
        isBlock = false;
    }

    public void FrameFrozen(float time)//攻击时间缩放
    {
        ffTimer = time;
        ffTimerTotal = time;     
    }


    void Special_Long()
    {
        if (Input.GetKeyDown(KeyCode.L)&& !isSpecial && !isAttack && !isBlock)
        {
            anim.Play("special_long");
            SpecialReady = false;
            isAttack = true;
            isSpecial = true;
        }

    }
    void SpecialOver()
    {
        isAttack = false;
        isSpecial = false;
    }

    void StartSpecial()
    {
         Instantiate(Specical, SpecialStartPoint.transform.position, transform.rotation);
    }
   
    //public void GenerateImpulse(float duration, float strength)
    //{
    //    MyInpulse.GenerateImpulse();
    //    Debug.Log("2323232323");
    //}

    //void SimpleRays()
    //{
    //    Ray ray = new Ray(transform.position, transform.forward);
    //    //RaycastHit2D[] hitInfos;

    //    hitInfo = Physics2D.RaycastAll(transform.position, transform.forward);//注意!Raycast是返回bool值，而这里则是返回一个数组
    //    Debug.DrawRay(transform.position, transform.forward * 100);

    //    //这里遍历输出检测到的游戏对象名字
    //    for (int i = 0; i < hitInfo.Length; i++)
    //    {
    //        Debug.Log(hitInfo[i].collider.gameObject.name);


    //    }
    //}

    //RaycastHit2D Raycast(Vector2 rayDiraction, float length, LayerMask layer)
    //{
    //    Vector2 pos = transform.position;

    //    hitInfo = Physics2D.Raycast(pos, rayDiraction, length, layer);

    //    // Debug.DrawRay(pos, rayDiraction * length);
    //    return hitInfo;    
    //}

    //void AttackCheck()
    //{
    //    RaycastHit2D LeftAttackCheck = Raycast(Vector2.left, 1f, IsEnemy);
    //    RaycastHit2D RightAttackCheck = Raycast(Vector2.right, 1f, IsEnemy);

    //   if ( RightAttackCheck && rb.transform.localScale.x==1 )
    //    {
    //      //  Debug.Log("111111111");


    //    }
    //    if (LeftAttackCheck && rb.transform.localScale.x == -1)
    //    {
    //     //   Debug.Log("222222222");

    //    }
    //    //for (int i = 0; i < hit.Length; i++)
    //    //{
    //    //    Debug.Log(hit[i].collider.gameObject.name);
    //    //}
    //}

    //public void Solide_TakeDamage()
    //{
    //    Debug.Log("11111111111111");

    //    if (transform.localScale.x > 0)
    //    {
    //        GetComponent<Enemy_Solider>().GetHit(Vector2.right);
    //    }
    //    else if (transform.localScale.x < 0)
    //    {
    //        GetComponent<Enemy_Solider>().GetHit(Vector2.left);
    //    }

    //    if (comboStep > 0)
    //    {
    //        GetComponent<Enemy_Solider>().TakeDamage(Damage);
    //    }
    //    else if (comboStep == 0)
    //    {
    //        GetComponent<Enemy_Solider>().SkillDamage(skillDamage);
    //    }
    //}

    //public void Flower_TakeDamage()
    //{
    //    if (transform.localScale.x > 0)
    //    {
    //        GetComponent<Enemy_Flower>().GetHit(Vector2.right);
    //    }
    //    else if (transform.localScale.x < 0)
    //    {
    //        GetComponent<Enemy_Flower>().GetHit(Vector2.left);
    //    }

    //    if (comboStep > 0)
    //    {
    //        GetComponent<Enemy_Flower>().TakeDamage(Damage);
    //    }
    //    else if (comboStep == 0)
    //    {
    //        GetComponent<Enemy_Flower>().SkillDamage(skillDamage);
    //    }
    //}
}
