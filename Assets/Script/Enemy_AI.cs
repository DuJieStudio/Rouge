using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { CloseRange ,LongRange }

public class Enemy_AI : MonoBehaviour
{
    public EnemyState enemyState;
    public Animator anim;
    public Vector2 sightRadius;//Overlap��ⷶΧ
    public Collider2D[] collider2ds;
    public GameObject attackTarget;
 
    private Rigidbody2D rb;
    public float patrolRange;
    private Vector2 wayPoint;
    public float cooldown = 0;
    private float randomX;
    private float attackTime = 2;

    private bool isWalk;
    private bool isAttack;
    private bool isIdle;

    private AnimatorStateInfo _animationStateInfo;

    void Awake()
    {
      
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {

    }

    void Update()
    {

        cooldown += Time.deltaTime;
        SwitchStates();
        SwitchAnimations();

        if(cooldown >= 3)
        {
            GetNewWayPoint();          
            cooldown = 0;
        }
        
    }

    void SwitchAnimations()
    {
       
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Attack", isAttack);
        anim.SetBool("Idle", isIdle);
        
    }

    bool FoundPlayer()
    {
        collider2ds = Physics2D.OverlapBoxAll(transform.position, sightRadius, 20);

        foreach (var target in collider2ds)
        {
            if (target.CompareTag("Player"))
            {

                attackTarget = target.gameObject;
                return true;  
                
            }           

        }
        attackTarget = null;
        return false;
    }

    void SwitchStates()
    {

        switch(enemyState)
        {

            case EnemyState.CloseRange:

                CloseRangeWay();

                break;

            case EnemyState.LongRange:

                LongRangeWay();

                break;

        } 
        
    }    

    void CloseRangeWay()
    {

        attackTime -= Time.deltaTime;

        //���ҵ���Ҳ�������ƶ�
        if (FoundPlayer())
        {          

            //�ڹ�����Χ��
            if (TargetInAttackRange() == true)
            {
                rb.velocity = new Vector2(0, 0);
                if (attackTime <= 0)
                {

                    isAttack = true;
                    attackTime = 2;
                    
                }
                else
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                    {
                        isAttack = false;
                       
                    }
                    isWalk = false;
                }

            }

            //���ڹ�����Χ��
            else
            {
                isWalk = true;
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                {
                    isAttack = false;                                    
                }
                
                if (rb.transform.position.x > attackTarget.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    rb.velocity = new Vector2(-3, 0);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    rb.velocity = new Vector2(3, 0);
                }

            }

        }

        //���Ѳ�ߵ�
        else
        {

            transform.Translate(wayPoint * Time.deltaTime * 3);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
            {
                isAttack = false;
            }
            isWalk = true;
            if (randomX > 0)
            {
                //transform.Translate(wayPoint * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);

            }
            else 
            { 
                transform.localScale = new Vector3(1, 1, 1);
            }

        }

        //��������
        //if (FoundPlayer() && TargetInAttackRange() == true)
        //{
        //    isWalk = false;
        //    rb.velocity = new Vector2(0, 0);

        //    if(attackTime <= 0)
        //    {

        //        isAttack = true;
        //        attackTime = 2;
        //        Debug.Log("CD����");

        //    }

        //}

        //������Χ���趨���ж�boolֵ
        bool TargetInAttackRange()
        {

            if (attackTarget != null)
                return Vector3.Distance(attackTarget.transform.position, transform.position) <= 1.5f;

            else
                return false;

        }

    }

    void LongRangeWay()
    {

        if (FoundPlayer())
        {

            if (rb.transform.position.x > attackTarget.transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(-3, 0);
            }
            //if (rb.transform.position.x < attackTarget.transform.position.x)
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(3, 0);
            }
            isWalk = true;
            isAttack = false;

        }

        else
        {

            transform.Translate(wayPoint * Time.deltaTime * 5);
            isWalk = true;
            if (randomX > 0)
            {
                //transform.Translate(wayPoint * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);

            }
            else { transform.localScale = new Vector3(1, 1, 1); }

        }

        if (FoundPlayer() && TargetInLongRange() == true)
        {

            rb.velocity = new Vector2(0, 0);
            isWalk = false;
            isAttack = true;

        }

        bool TargetInLongRange()
        {

            if (attackTarget != null)
                return Vector3.Distance(attackTarget.transform.position, transform.position) <= 10;

            else
                return false;

        }

    }

    //���Ѳ�ߵ�Ļ�ȡ
    void GetNewWayPoint()
    {

        randomX = Random.Range(-patrolRange, patrolRange);
        Vector2 randomPoint = new Vector2(randomX, 0);
        wayPoint = randomPoint;

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(10,2));
    }

    void isattackScueed()
    {
        rb.velocity = new Vector2(0, 0);
    }

}
