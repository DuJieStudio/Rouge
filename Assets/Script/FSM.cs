using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StateType   //声明枚举类型
{
    Idle, Patrol, Chase, React, Attack, Hit, Death
}


[Serializable]//序列化

public class Parameter//新建一个类来声明敌人的各种参数
{
    public int health;
    public float moveSpeed;
    public float chaseSpeed;
    public float IdleTime;
    public Transform[] patrolPoints;//巡逻范围
    public Transform[] chasePoint;//追击范围。。声明生命值等参数

    public Transform target;
    public LayerMask targetLayer;

    public Transform attackPoint;

    public float attackArea;
    public Animator animator;//声明一个动画器组件来管理动画

    public bool getHit;
    
}

public class FSM : MonoBehaviour//状态机脚本
{
    public Parameter parameter;//用于在监视面板中看到这个类并编辑其中参数

    private IState currentState; //声明当前状态
    private Dictionary<StateType, IState>  states = new Dictionary<StateType, IState>();//使用字典注册所有状态,使用刚创建的StaateType作为字典的Key

    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));//增加字典的键值对
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Hit, new HitState(this));
        states.Add(StateType.Death, new DeathState(this));

        TransitionState(StateType.Idle);//用transitionstate函数设置初始状态为idle

        parameter.animator = GetComponent<Animator>();//最后获取动画器并在update函数中持续运行当前状态的onupdate函数

    }

   
    void Update()
    {
        currentState.OnUpdate();

        if(Input.GetKeyDown(KeyCode.Return))
        {
            parameter.getHit = true;
        }
    }

    public void TransitionState(StateType type)//切换状态函数
    {
        if (currentState != null)//在转移状态时要首先执行前一个状态的OnExit函数
            currentState.OnExit();
        currentState = states[type]; //然后将当前状态切换为给定状态
        currentState.OnEnter();//最后执行新状态OnEnter函数
    }

    //将各个状态通用的函数写在这个脚本中
    public void FlipTo(Transform target)
    {
        if (target != null)//判断朝向
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))//物体进入范围时判断标签
        {
            parameter.target = other.transform;//是，就把玩家的Tranform赋给target让敌人知道玩家信息
        }
    }
    private void OnTriggerExit2D(Collider2D other)//用这个函数在玩家推出范围后重置为空
    {
        if(other.CompareTag("Player"))
        {

            parameter.target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);//
    }
}
