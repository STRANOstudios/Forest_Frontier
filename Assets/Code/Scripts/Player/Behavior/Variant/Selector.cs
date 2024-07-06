using System.Collections.Generic;

public class Selector : Node
{
    private List<Node> children;
    private int currentChild = 0;

    public Selector(List<Node> children)
    {
        this.children = children;
    }

    public override Status Execute()
    {
        while (currentChild < children.Count)
        {
            Status childStatus = children[currentChild].Execute();

            if (childStatus == Status.Running)
            {
                return Status.Running;
            }

            if (childStatus == Status.Success)
            {
                currentChild = 0;
                return Status.Success;
            }

            currentChild++;
        }

        currentChild = 0;
        return Status.Failure;
    }
}
