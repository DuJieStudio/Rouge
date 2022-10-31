using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { CloseRange ,LongRange }

public class Enemy_AI : MonoBehaviour
{

    [Header("��������")]
    public EnemyState enemyState;

    [Header("����ʶ��Χ")]
    public Vector2 sightRadius;//Overlap��ⷶΧ   

    [Header("����Ѳ�߷�Χ")]
    public float patrolRange;
    [Tooltip("Ѳ�ߵ����CD")]
    public float cooldown = 0;//Ѳ�ߵ��ȡʱ��
    
    [Header("���˹�������")]
    [Tooltip("����CD")]
    public float attackTime = 2;
    [Tooltip("������Χ")]
    public float attackRange;

    [Header("���ƹ�����Χ����")]
    public Vector2 vector2;

    //����Boolֵ�ж�
    private bool isWalk;
    private bool isAttack;

    //�����ȡ
    private Vector2 wayPoint;
    private Rigidbody2D rb;
    private float randomX;//���Ѳ��Xֵ

    [HideInInspector]
    public GameObject attackTarget;//����Ŀ��
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Collider2D[] collider2ds;//overlap��ײ����

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
        Debug.Log(isWalk);
        Debug.Log(isAttack);
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
        
    }

    bool FoundPlayer()
    {
        collider2ds = Physics2D.OverlapBoxAll(transform.position, sightRadius, 0);

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

            PartrolMove();

        }

        //������Χ��ȡ
        bool TargetInAttackRange()
        {

            if (attackTarget != null)
                return Vector3.Distance(attackTarget.transform.position, transform.position) <= attackRange;

            else
                return false;

        }

    }

    void LongRangeWay()
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
                    attackTime = 5;

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

            PartrolMove();

        }

        //������Χ��ȡ
        bool TargetInAttackRange()
        {

            if (attackTarget != null)
                return Vector3.Distance(attackTarget.transform.position, transform.position) <= attackRange;

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

    void PartrolMove()
    {
        
        transform.Translate(wayPoint * Time.deltaTime);
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

    void isattackScueed()
    {
        rb.velocity = new Vector2(0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, vector2);
    }

}
