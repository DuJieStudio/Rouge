using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState//����״̬��������ű���ʵ��
{
    private FSM manager;//��Ӷ�״̬��������
    private Parameter parameter;//���һ�����Զ�������ȡ��������

    private float timer;//����һ����ʱ��


    public IdleState(FSM manager)//�ڹ��캯���л�ȡ��״̬������ͨ��״̬�������ȡ����
    {
        this.manager = manager;
        this.parameter = manager.parameter;

    }

    public void OnEnter()
    {
        parameter.animator.Play("Idle");

    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if (parameter.target != null &&
            parameter.target.position.x >= parameter.chasePoint[0].position.x &&
            parameter.target.position.x <= parameter.chasePoint[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }

        if (timer >= parameter.IdleTime)
        {
            manager.TransitionState(StateType.Patrol);//��ʱ�������趨�õ�ʱ���л�ΪѲ��״̬
        }
    }

    public void OnExit()
    {
        timer = 0;//�˳�ʱ��ռ�ʱ��
    }
}

public class PatrolState : IState//����״̬��������ű���ʵ��
{
    private FSM manager;//��Ӷ�״̬��������
    private Parameter parameter;//���һ�����Զ�������ȡ��������

    private int patrolPosition;//Ѳ��״̬,�����±���Һ��л�Ѳ�ߵ�

    public PatrolState(FSM manager)//�ڹ��캯���л�ȡ��״̬������ͨ��״̬�������ȡ����
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Walk");
    }

    public void OnUpdate()
    {
        //�õ��˳�������Ѳ�߷���
        manager.FlipTo(parameter.patrolPoints[patrolPosition]);
        //���µ���λ���õ����ƶ���Ŀ��Ѳ�ߵ�
        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);

        if (parameter.target != null &&
            parameter.target.position.x >= parameter.chasePoint[0].position.x &&
            parameter.target.position.x <= parameter.chasePoint[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }

        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < .1f)
        {
            manager.TransitionState(StateType.Idle);//����Ѳ�ߵ���л���idle״̬
        }
    }

    public void OnExit()
    {
        patrolPosition++;//����Ѳ�ߵ���±���ȷ����һ��Ѳ�ߵ�

        if (patrolPosition >= parameter.patrolPoints.Length)//�ж��±��Ƿ񳬳����鷶Χ
        {
            patrolPosition = 0;
        }
    }
}

public class ChaseState : IState//����״̬��������ű���ʵ��
{
    private FSM manager;//��Ӷ�״̬��������
    private Parameter parameter;//���һ�����Զ�������ȡ��������

    public ChaseState(FSM manager)//�ڹ��캯���л�ȡ��״̬������ͨ��״̬�������ȡ����
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Walk");
    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.target);//׷��������ʼ�ճ������
        if (parameter.target)
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                parameter.target.position, parameter.chaseSpeed * Time.deltaTime);//����Ŀ���ʱ����׷���ٶȿ���Ŀ��

        if(parameter.target == null ||
            manager.transform.position.x < parameter.chasePoint[0].position.x ||
            manager.transform.position.x > parameter.chasePoint[1].position.x)//��ʧĿ��򳬳�׷����Χ���ػ�Ѳ��״̬
        {
            manager.TransitionState(StateType.Idle);
        }

        if (Physics2D.OverlapCircle(parameter.attackPoint.position,parameter.attackArea,parameter.targetLayer))//�����������⹥����Χ������Բ��λ�ã��뾶�����ͼ��
        {
            manager.TransitionState(StateType.Attack);//��⵽��Ҿ��л�������״̬
        }
    }

    public void OnExit()
    {

    }
}
public class ReactState : IState//����״̬��������ű���ʵ��(��Ӧ״̬�Ĵ��룩
{
    private FSM manager;//��Ӷ�״̬��������
    private Parameter parameter;//���һ�����Զ�������ȡ��������

    private AnimatorStateInfo info;

    public ReactState(FSM manager)//�ڹ��캯���л�ȡ��״̬������ͨ��״̬�������ȡ����
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Walk");//�޸Ĺ���һ����ʱ��״̬����������������������������������������������������������������������
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if(info.normalizedTime >= .95f)//������ɺ��л���׷��״̬
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {

    }
}
public class AttackState : IState//����״̬��������ű���ʵ��
{
    private FSM manager;//��Ӷ�״̬��������
    private Parameter parameter;//���һ�����Զ�������ȡ��������

    private AnimatorStateInfo info;

    public AttackState(FSM manager)//�ڹ��캯���л�ȡ��״̬������ͨ��״̬�������ȡ����
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Attack");
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= .95)
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {

    }
}

    public class HitState : IState//����״̬��������ű���ʵ��
    {
        private FSM manager;//��Ӷ�״̬��������
        private Parameter parameter;//���һ�����Զ�������ȡ��������

        private AnimatorStateInfo info;

        public HitState(FSM manager)//�ڹ��캯���л�ȡ��״̬������ͨ��״̬�������ȡ����
        {
            this.manager = manager;
            this.parameter = manager.parameter;
        }

        public void OnEnter()
        {
            
        }

        public void OnUpdate()
        {
            
        }

        public void OnExit()
        {

        }
    }

public class DeathState : IState//����״̬��������ű���ʵ��
{
    private FSM manager;//��Ӷ�״̬��������
    private Parameter parameter;//���һ�����Զ�������ȡ��������

    private AnimatorStateInfo info;

    public DeathState(FSM manager)//�ڹ��캯���л�ȡ��״̬������ͨ��״̬�������ȡ����
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}

