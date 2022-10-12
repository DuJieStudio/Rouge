
public interface IState //状态机，声明状态类，一个基本状态需要三个需要执行的事件
{
    void OnEnter();//进入使


    void OnUpdate();//执行时


    void OnExit();//退出时
}
