using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StateType   //����ö������
{
    Idle, Patrol, Chase, React, Attack, Hit, Death
}


[Serializable]//���л�

public class Parameter//�½�һ�������������˵ĸ��ֲ���
{
    public int health;
    public float moveSpeed;
    public float chaseSpeed;
    public float IdleTime;
    public Transform[] patrolPoints;//Ѳ�߷�Χ
    public Transform[] chasePoint;//׷����Χ������������ֵ�Ȳ���

    public Transform target;
    public LayerMask targetLayer;

    public Transform attackPoint;

    public float attackArea;
    public Animator animator;//����һ�������������������

    public bool getHit;
    
}

public class FSM : MonoBehaviour//״̬���ű�
{
    public Parameter parameter;//�����ڼ�������п�������ಢ�༭���в���

    private IState currentState; //������ǰ״̬
    private Dictionary<StateType, IState>  states = new Dictionary<StateType, IState>();//ʹ���ֵ�ע������״̬,ʹ�øմ�����StaateType��Ϊ�ֵ��Key

    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));//�����ֵ�ļ�ֵ��
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Hit, new HitState(this));
        states.Add(StateType.Death, new DeathState(this));

        TransitionState(StateType.Idle);//��transitionstate�������ó�ʼ״̬Ϊidle

        parameter.animator = GetComponent<Animator>();//����ȡ����������update�����г������е�ǰ״̬��onupdate����

    }

   
    void Update()
    {
        currentState.OnUpdate();

        if(Input.GetKeyDown(KeyCode.Return))
        {
            parameter.getHit = true;
        }
    }

    public void TransitionState(StateType type)//�л�״̬����
    {
        if (currentState != null)//��ת��״̬ʱҪ����ִ��ǰһ��״̬��OnExit����
            currentState.OnExit();
        currentState = states[type]; //Ȼ�󽫵�ǰ״̬�л�Ϊ����״̬
        currentState.OnEnter();//���ִ����״̬OnEnter����
    }

    //������״̬ͨ�õĺ���д������ű���
    public void FlipTo(Transform target)
    {
        if (target != null)//�жϳ���
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
        if(other.CompareTag("Player"))//������뷶Χʱ�жϱ�ǩ
        {
            parameter.target = other.transform;//�ǣ��Ͱ���ҵ�Tranform����target�õ���֪�������Ϣ
        }
    }
    private void OnTriggerExit2D(Collider2D other)//���������������Ƴ���Χ������Ϊ��
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
