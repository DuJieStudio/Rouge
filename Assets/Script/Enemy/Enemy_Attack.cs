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
    [Header("数据")]
    public SoliderData_SO Attackdata;
    public GameObject thisobject;
    public LayerMask player;
    public PlayerData_SO playerdata;
    public void Awake()
    {
        thisobject = this.gameObject;
        Attackdata = thisobject.GetComponent<EnemySoliderStats>().SoliderData;
        attackrate = Attackdata.attackrate;
        attacktime = 1 / attackrate;
        playerdata = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().PlayerData;
        damagetime = attacktime;
        damage = Attackdata.damage;
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
        GameObject enemyattack;
        enemyattack = GameObject.Find("enemyAttack");
        if (Physics2D.OverlapCircle(thisobject.transform.position, 0.8f, player))
        {
            if (damagetime >= attacktime)
            {
                playerdata.currenthealth -= damage;
                damagetime = 0;

            }
            damagetime += Time.deltaTime;
        }
       
        
    }
    void TouchAttack()
    {
        if (Physics2D.OverlapCircle(thisobject.transform.position, 0.2f, player))
        {
            if (damagetime >= attacktime)
            {
                playerdata.currenthealth -= damage;
                damagetime = 0;
                
            }
            damagetime += Time.deltaTime;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {

        }
    }
    void RemoteAttack()
    {
        bool isstart;
        isstart = GetComponent<PoisonBallController>().isStart;
        if (Physics2D.OverlapCircle(thisobject.transform.position, 2f, player)&&isstart)
        {
            if (damagetime >= attacktime)
            {
                playerdata.currenthealth -= damage;
                damagetime = 0;

            }
            damagetime += Time.deltaTime;
        }

    }
}
