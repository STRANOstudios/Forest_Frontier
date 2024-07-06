using System.Collections.Generic;

public class Sequence : Node
{
    private List<Node> children;
    private int currentChild = 0;

    public Sequence(List<Node> children)
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

            if (childStatus == Status.Failure)
            {
                currentChild = 0;
                return Status.Failure;
            }

            currentChild++;
        }

        currentChild = 0;
        return Status.Success;
    }
}
