using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState//所有状态类在这个脚本中实现
{
    private FSM manager;//添加对状态机的引用
    private Parameter parameter;//添加一个属性对象来获取设置属性

    private float timer;//设置一个计时器


    public IdleState(FSM manager)//在构造函数中获取到状态机对象并通过状态机对象获取属性
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
            manager.TransitionState(StateType.Patrol);//计时器到达设定好的时间切换为巡逻状态
        }
    }

    public void OnExit()
    {
        timer = 0;//退出时清空计时器
    }
}

public class PatrolState : IState//所有状态类在这个脚本中实现
{
    private FSM manager;//添加对状态机的引用
    private Parameter parameter;//添加一个属性对象来获取设置属性

    private int patrolPosition;//巡逻状态,用于下标查找和切换巡逻点

    public PatrolState(FSM manager)//在构造函数中获取到状态机对象并通过状态机对象获取属性
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
        //让敌人持续朝向巡逻方向
        manager.FlipTo(parameter.patrolPoints[patrolPosition]);
        //更新敌人位置让敌人移动到目标巡逻点
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
            manager.TransitionState(StateType.Idle);//到达巡逻点后切换到idle状态
        }
    }

    public void OnExit()
    {
        patrolPosition++;//增加巡逻点的下标来确定下一个巡逻点

        if (patrolPosition >= parameter.patrolPoints.Length)//判断下标是否超出数组范围
        {
            patrolPosition = 0;
        }
    }
}

public class ChaseState : IState//所有状态类在这个脚本中实现
{
    private FSM manager;//添加对状态机的引用
    private Parameter parameter;//添加一个属性对象来获取设置属性

    public ChaseState(FSM manager)//在构造函数中获取到状态机对象并通过状态机对象获取属性
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
        manager.FlipTo(parameter.target);//追击过程中始终朝向玩家
        if (parameter.target)
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                parameter.target.position, parameter.chaseSpeed * Time.deltaTime);//存在目标的时候用追击速度靠近目标

        if(parameter.target == null ||
            manager.transform.position.x < parameter.chasePoint[0].position.x ||
            manager.transform.position.x > parameter.chasePoint[1].position.x)//丢失目标或超出追击范围后重回巡逻状态
        {
            manager.TransitionState(StateType.Idle);
        }

        if (Physics2D.OverlapCircle(parameter.attackPoint.position,parameter.attackArea,parameter.targetLayer))//用这个函数检测攻击范围，传入圆心位置，半径，玩家图层
        {
            manager.TransitionState(StateType.Attack);//检测到玩家就切换到攻击状态
        }
    }

    public void OnExit()
    {

    }
}
public class ReactState : IState//所有状态类在这个脚本中实现(反应状态的代码）
{
    private FSM manager;//添加对状态机的引用
    private Parameter parameter;//添加一个属性对象来获取设置属性

    private AnimatorStateInfo info;

    public ReactState(FSM manager)//在构造函数中获取到状态机对象并通过状态机对象获取属性
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Walk");//修改过第一个点时的状态；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if(info.normalizedTime >= .95f)//播放完成后切换致追击状态
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {

    }
}
public class AttackState : IState//所有状态类在这个脚本中实现
{
    private FSM manager;//添加对状态机的引用
    private Parameter parameter;//添加一个属性对象来获取设置属性

    private AnimatorStateInfo info;

    public AttackState(FSM manager)//在构造函数中获取到状态机对象并通过状态机对象获取属性
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

    public class HitState : IState//所有状态类在这个脚本中实现
    {
        private FSM manager;//添加对状态机的引用
        private Parameter parameter;//添加一个属性对象来获取设置属性

        private AnimatorStateInfo info;

        public HitState(FSM manager)//在构造函数中获取到状态机对象并通过状态机对象获取属性
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

public class DeathState : IState//所有状态类在这个脚本中实现
{
    private FSM manager;//添加对状态机的引用
    private Parameter parameter;//添加一个属性对象来获取设置属性

    private AnimatorStateInfo info;

    public DeathState(FSM manager)//在构造函数中获取到状态机对象并通过状态机对象获取属性
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

