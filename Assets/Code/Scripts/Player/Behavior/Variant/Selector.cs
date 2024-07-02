using System.Collections.Generic;

public class Selector : Node
{
    private List<Node> children = new List<Node>();

    public Selector(List<Node> children)
    {
        this.children = children;
    }

    public override bool Execute()
    {
        foreach (Node child in children)
        {
            if (child.Execute())
            {
                return true; // Succeed if any child succeeds
            }
        }
        return false; // Fail if all children fail
    }
}
