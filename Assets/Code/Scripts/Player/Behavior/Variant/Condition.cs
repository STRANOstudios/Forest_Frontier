public class IsDayTime : Node
{
    public override bool Execute()
    {
        return BehaviorTree2.Instance.sim.IsDayTime();
    }
}

public class IsNightTime : Node
{
    public override bool Execute()
    {
        return BehaviorTree2.Instance.sim.IsNightTime();
    }
}

public class IsHungry : Node
{
    public override bool Execute()
    {
        return BehaviorTree2.Instance.sim.IsHungry();
    }
}

public class IsThirsty : Node
{
    public override bool Execute()
    {
        return BehaviorTree2.Instance.sim.IsThirsty();
    }
}

public class HasTarget : Node
{
    public override bool Execute()
    {
        return BehaviorTree2.Instance.sim.HasTarget();
    }
}

public class FindNearestTree : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.FindNearestTree();
        return true;
    }
}

public class MoveToTree : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.MoveToTarget();
        return BehaviorTree2.Instance.sim.IsAtTarget();
    }
}

public class IsTreeActive : Node
{
    public override bool Execute()
    {
        return BehaviorTree2.Instance.sim.IsTreeActive();
    }
}

public class ChopAction : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.ChopTree();
        return true;
    }
}

public class IsBackpackFull : Node
{
    public override bool Execute()
    {
        return BehaviorTree2.Instance.sim.IsBackpackFull();
    }
}

public class GoToStorage : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.GoToStorage();
        return BehaviorTree2.Instance.sim.IsAtTarget();
    }
}

public class DepositLogs : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.DepositLogs();
        return true;
    }
}

public class GoToSleep : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.GoToSleep();
        return true;
    }
}

public class HasLogsInBackpack : Node
{
    public override bool Execute()
    {
        return BehaviorTree2.Instance.sim.HasLogsInBackpack();
    }
}

public class FindFood : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.FindNearestFood();
        return true;
    }
}

public class ConsumeFood : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.ConsumeFood();
        return true;
    }
}

public class FindWater : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.FindNearestWater();
        return true;
    }
}

public class ConsumeWater : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.ConsumeWater();
        return true;
    }
}

public class MoveToTarget : Node
{
    public override bool Execute()
    {
        BehaviorTree2.Instance.sim.MoveToTarget();
        return true;
    }
}