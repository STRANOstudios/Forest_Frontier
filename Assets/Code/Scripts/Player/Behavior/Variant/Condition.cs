
#region Conditions

using System.Diagnostics;

public class IsDayTime : Node
{
    public override Status Execute()
    {
        return BehaviorTree2.Instance.sim.IsDayTime() ? Status.Success : Status.Failure;
    }
}

public class IsNightTime : Node
{
    public override Status Execute()
    {
        return BehaviorTree2.Instance.sim.IsNightTime() ? Status.Success : Status.Failure;
    }
}

public class IsHungry : Node
{
    public override Status Execute()
    {
        return BehaviorTree2.Instance.sim.IsHungry() ? Status.Success : Status.Failure;
    }
}

public class IsThirsty : Node
{
    public override Status Execute()
    {
        return BehaviorTree2.Instance.sim.IsThirsty() ? Status.Success : Status.Failure;
    }
}

public class HasTarget : Node
{
    public override Status Execute()
    {
        return BehaviorTree2.Instance.sim.HasTarget() ? Status.Success : Status.Failure;
    }
}

public class IsTreeActive : Node
{
    public override Status Execute()
    {
        return BehaviorTree2.Instance.sim.IsTreeActive() ? Status.Success : Status.Failure;
    }
}

public class IsBackpackFull : Node
{
    public override Status Execute()
    {
        return BehaviorTree2.Instance.sim.IsBackpackFull() ? Status.Success : Status.Failure;
    }
}

public class HasLogsInBackpack : Node
{
    public override Status Execute()
    {
        return BehaviorTree2.Instance.sim.HasLogsInBackpack() ? Status.Success : Status.Failure;
    }
}

#endregion

#region Actions

public class FindNearestTree : Node
{
    public override Status Execute()
    {
        Sim sim = BehaviorTree2.Instance.sim;
        sim.FindNearestTree();
        return sim.HasTarget() ? Status.Success : Status.Failure;
    }
}

public class MoveToTree : Node
{
    public override Status Execute()
    {
        Sim sim = BehaviorTree2.Instance.sim;
        sim.MoveToTarget();
        return sim.IsAtTarget() ? Status.Success : Status.Running;
    }
}

public class ChopAction : Node
{
    public override Status Execute()
    {
        Sim sim = BehaviorTree2.Instance.sim;
        sim.ChopTree();
        return !sim.IsTreeActive() ? Status.Success : Status.Running;
    }
}

public class GoToStorage : Node
{
    public override Status Execute()
    {
        Sim sim = BehaviorTree2.Instance.sim;
        sim.GoToStorage();
        return sim.IsAtTarget() ? Status.Success : Status.Running;
    }
}

public class DepositLogs : Node
{
    public override Status Execute()
    {
        Sim sim = BehaviorTree2.Instance.sim;
        sim.DepositLogs();
        return Status.Success;
    }
}

public class GoToSleep : Node
{
    public override Status Execute()
    {
        BehaviorTree2.Instance.sim.GoToSleep();
        return Status.Success;
    }
}

public class FindFood : Node
{
    public override Status Execute()
    {
        BehaviorTree2.Instance.sim.FindNearestFood();
        return Status.Success;
    }
}

public class ConsumeFood : Node
{
    public override Status Execute()
    {
        BehaviorTree2.Instance.sim.ConsumeFood();
        return Status.Success;
    }
}

public class FindWater : Node
{
    public override Status Execute()
    {
        BehaviorTree2.Instance.sim.FindNearestWater();
        return Status.Success;
    }
}

public class ConsumeWater : Node
{
    public override Status Execute()
    {
        BehaviorTree2.Instance.sim.ConsumeWater();
        return Status.Success;
    }
}

public class MoveToTarget : Node
{
    public override Status Execute()
    {
        Sim sim = BehaviorTree2.Instance.sim;
        sim.MoveToTarget();
        return sim.IsAtTarget() ? Status.Success : Status.Running;
    }
}


#endregion