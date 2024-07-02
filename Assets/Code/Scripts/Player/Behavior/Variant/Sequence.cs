using System.Collections.Generic;

public class Sequence : Node
{
    private List<Node> children = new List<Node>();

    public Sequence(List<Node> children)
    {
        this.children = children;
    }

    public override bool Execute()
    {
        foreach (Node child in children)
        {
            if (!child.Execute())
            {
                return false; // Fail if any child fails
            }
        }
        return true; // Succeed if all children succeed
    }
}
