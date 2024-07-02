
using System.Collections.Generic;

namespace BehaviorTree
{
    public class Node
    {
        public enum Status { Success, Failure, Running }
        public Status status;

        public readonly List<Node> children = new();
        protected int currentChild = 0;

        public string name;

        public Node() { }

        public Node(string _name = "Node")
        {
            name = _name;
        }

        public void AddChild(Node node) => children.Add(node);

        public virtual Status Process() => children[currentChild].Process();

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (var child in children)
            {
                child.Reset();
            }
        }
    }

}
