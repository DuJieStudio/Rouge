using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType { Melee,Touch,Remote}
public class Enemy_Attack : MonoBehaviour
{
    [Header("敌人类型")]
    public EnemyType EnemyType;

    [Header("攻击属性")]
    public float attackrate;//每秒攻击频率
    public float attacktime;//每次攻击所用时间
    public float damagetime;//受伤时间
    public float damage;
    public int force;
    [Header("数据")]
    public SoliderData_SO Attackdata;
    public GameObject thisobject;
    public LayerMask player;
    public PlayerData_SO playerdata;
    private bool ismelee;
    public void Awake()
    {
        thisobject = this.gameObject;
        Attackdata = thisobject.GetComponent<EnemySoliderStats>().SoliderData;
        attackrate = Attackdata.attackrate;
        attacktime = 1 / attackrate;
        playerdata = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().PlayerData;
        damagetime = attacktime;
        damage = 0;
        force = Attackdata.force;
    }
    public void Update()
    {
        SwitchType();
    }
    void SwitchType()
    {
        switch (EnemyType)
        {
            case EnemyType.Melee:
                MeleeAttack();
                break;
            case EnemyType.Touch:
                TouchAttack();
                break;
            case EnemyType.Remote:
                RemoteAttack();
                break;
        }
    }
    void MeleeAttack()
    {
        
    }
    void TouchAttack()
    {
        if (Physics2D.OverlapCircle(thisobject.transform.position, 0.2f, player))
        {
            if (damagetime >= attacktime)
            {
                playerdata.currenthealth -= (force*2);
                damagetime = 0;
                
            }
            damagetime += Time.deltaTime;
        }

    }
    void RemoteAttack()
    { 
        damagetime += Time.deltaTime;
        bool isstart;
        isstart = GetComponent<PoisonBallController>().isStart;
        if (Physics2D.OverlapCircle(thisobject.transform.position, 2f, player)&&isstart)
        {
            if (damagetime >= attacktime)
            {
                playerdata.currenthealth -= (0.8f*force)+Random.Range(-1,1);
                damagetime = 0;

            }
           
        }
        
    }
    public void isMeleeAttack()
    {
        ismelee = true;
        
    }
    public void endmelleeAttack()
    {
        ismelee = false;   
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&GameObject.Find("Enemy_Solider").GetComponent<Enemy_Solider>().IsAttack)
        {
            playerdata.currenthealth -= (force*1)+Random.Range(1,-2);
        }
    }
}
