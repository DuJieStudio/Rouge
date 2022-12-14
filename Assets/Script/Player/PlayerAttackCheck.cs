using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttackCheck : MonoBehaviour
{
    public GameObject Target;
    public Rigidbody2D rb;
    public float useTime = 0;
    public float coolDownTime=4f;
    public Attack GetAttack;
    //  public Collider2D[] collider2ds;//overlapÅö×²Ìå´æ·Å
    
    public float totalDamage;//ÀÛ¼ÆÉËº¦
    public float test;
   

    [Header("ÆÕÍ¨¹¥»÷·¶Î§Ïà¹Ø")]
    public float x_normal;//ÆÕ¹¥·¶Î§ÖÐÐÄÐÞÕý
    public Vector3 normalattack;//ÆÕÍ¨¹¥»÷¼ì²â¿ò·¶Î§ÖÐÐÄ
    public Vector2 normalAttackCheck;//ÆÕÍ¨¹¥»÷Overlap¼ì²â·¶Î§ 
    public Collider2D[] collider2ds;//overlapÅö×²Ìå´æ·Å

    [Header("skill_short·¶Î§Ïà¹Ø")]
    public float x_skillS;
    public Vector3 skillSattack;
    public Vector2 skillSAttackCheck;
    public Collider2D[] collider2ds_skillS;

   [Header("skill_long·¶Î§Ïà¹Ø")]
    public float x_skillL;
    public float y_skillL;
    public Vector3 skillLattack;
    public Vector2 skillLAttackCheck;
    public Collider2D[] collider2ds_skillL;

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        GetAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }

    private void FixedUpdate()
    {
        if (GetAttack.ffTimer > 0)//¹¥»÷Ê±Ëõ·ÅÊ±¼äÓÃ
        {
            GetAttack.ffTimer -= Time.deltaTime;
            Time.timeScale = Mathf.Lerp(0.5f, 1f, (1 - (GetAttack.ffTimer / GetAttack.ffTimerTotal)));
        }
    }

    void Update()
    {
        CheckRange();     
        test = Normal_Damage();
        //  collider2ds = Physics2D.OverlapBoxAll(normalattack, normalAttackCheck, 0);
        //OnDrawGizmos();

        AboutSpecialReady();
        Debug.Log(GetAttack.SpecialReady);



        if (useTime != 0)
        {
            coolDownTime -= Time.deltaTime;
            if (coolDownTime <= 0)
            {
                coolDownTime = 4f;
                useTime = 0;
            }
        }
        else if (useTime == 0)
        {
            coolDownTime = 4f;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(normalattack, normalAttackCheck);
        Gizmos.DrawWireCube(skillSattack, skillSAttackCheck);
        Gizmos.DrawWireCube(skillLattack, skillLAttackCheck);
    }

    void CheckRange()
    {
        if (rb.transform.localScale.x == 1)
        {
            normalattack = new Vector3(transform.position.x + x_normal, transform.position.y, transform.position.z);
            skillSattack = new Vector3(transform.position.x + x_skillS, transform.position.y, transform.position.z);
            skillLattack = new Vector3(transform.position.x + x_skillL, transform.position.y + y_skillL, transform.position.z);
        }
        else
        {
            normalattack = new Vector3(transform.position.x - x_normal, transform.position.y, transform.position.z);
            skillSattack = new Vector3(transform.position.x - x_skillS, transform.position.y, transform.position.z);
            skillLattack = new Vector3(transform.position.x - x_skillL, transform.position.y + y_skillL, transform.position.z);
        }
    }

    public void NormalAttack()
    {
        collider2ds = Physics2D.OverlapBoxAll(normalattack, normalAttackCheck, 0);
        foreach (var target in collider2ds)
        {
            if (target.CompareTag("enemy"))
            {
                Target = target.gameObject;
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Solider)
                {
                    Target.GetComponent<Enemy_Solider>().TakeDamage(Normal_Damage());
                    Target.GetComponent<Enemy_Solider>().GetHit();
                    totalDamage += Normal_Damage();            
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Flower)
                {
                    Target.GetComponent<Enemy_Flower>().TakeDamage(Normal_Damage());
                    Target.GetComponent<Enemy_Flower>().GetHit();
                    totalDamage += Normal_Damage();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Ghost)
                {
                    Target.GetComponent<Ghost>().TakeDamage(Normal_Damage());
                    Target.GetComponent<Ghost>().GetHit();
                    totalDamage += Normal_Damage();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Light)
                {
                    Target.GetComponent<Enemy_Light>().TakeDamage(Normal_Damage());
                    Target.GetComponent<Enemy_Light>().GetHit();
                    totalDamage += Normal_Damage();
                }
                CameraShaker.Instance.ShakeCamera(1f, 1.2f, 0.15f);
                GetAttack.FrameFrozen(0.5f);
            }
        }
    }

    public void SkillShortAttack()
    {
        collider2ds_skillS = Physics2D.OverlapBoxAll(skillSattack, skillSAttackCheck, 0);
        foreach (var target in collider2ds_skillS)
        {
            if (target.CompareTag("enemy"))
            {
                Target = target.gameObject;

                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Solider)
                {
                    Target.GetComponent<Enemy_Solider>().SkillDamage(Skill_Damage());
                    Target.GetComponent<Enemy_Solider>().GetHit();

                    totalDamage += Skill_Damage();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Flower)
                {
                    Target.GetComponent<Enemy_Flower>().SkillDamage(Skill_Damage());
                    Target.GetComponent<Enemy_Flower>().GetHit();
                    totalDamage += Skill_Damage();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Ghost)
                {
                    Target.GetComponent<Ghost>().SkillDamage(Skill_Damage());
                    Target.GetComponent<Ghost>().GetHit();
                    totalDamage += Skill_Damage();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Light)
                {
                    Target.GetComponent<Enemy_Light>().SkillDamage(Skill_Damage());
                    Target.GetComponent<Enemy_Light>().GetHit();
                    totalDamage += Skill_Damage();
                }
                CameraShaker.Instance.ShakeCamera(1.5f, 2f, 0.3f);
                GetAttack.FrameFrozen(0.55f);
            }
        }
    }


    public void SkillLongAttack()
    {
        collider2ds_skillL = Physics2D.OverlapBoxAll(skillLattack, skillLAttackCheck, 0);
        CameraShaker.Instance.ShakeCamera(2f, 2.5f, 0.3f);
        GetAttack.FrameFrozen(0.55f);

        InvokeRepeating("SkillLongDamage", 0f, 0.3f);

    }

    private void SkillLongDamage()
    {
        useTime += 1;
        if (useTime == 5)
        {
            useTime = 0;
            this.CancelInvoke();
        }

        foreach (var target in collider2ds_skillL)
        {
           
            if (target.CompareTag("enemy"))
            {
                Target = target.gameObject;

                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Solider)
                {
                    Target.GetComponent<Enemy_Solider>().SkillDamage(Skill_Damage());
                    Target.GetComponent<Enemy_Solider>().GetHit();
                    totalDamage += Skill_Damage();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Flower)
                {
                    Target.GetComponent<Enemy_Flower>().SkillDamage(Skill_Damage());
                    Target.GetComponent<Enemy_Flower>().GetHit();
                    totalDamage += Skill_Damage();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Ghost)
                {
                    Target.GetComponent<Ghost>().SkillDamage(Skill_Damage());
                    Target.GetComponent<Ghost>().GetHit();
                    totalDamage += Skill_Damage();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Light)
                {
                    Target.GetComponent<Enemy_Light>().SkillDamage(Skill_Damage());
                    Target.GetComponent<Enemy_Light>().GetHit();
                    totalDamage += Skill_Damage();
                }                              
            }       
        }   
    }


    private float Normal_Damage()
    {
        float damage = 1f * GetAttack.Power + Random.Range(0, 4);
        return damage;
    }

    private float Skill_Damage()
    {
        float damage = 1.5f * GetAttack.Power + UnityEngine.Random.Range(-3, 3);
        return damage;
    }

    public void AboutSpecialReady()
    {
        if (GetAttack.SpecialReady == true)
        {
            totalDamage = 0;
        }
    }
}
