using UnityEngine;

namespace BehaviorTree
{
    public class Leaf : Node
    {
        public delegate Status Tick();
        public Tick ProcessMethod;

        public Leaf() { }

        public Leaf(string _name, Tick _method)
        {
            name = _name;
            ProcessMethod = _method;
        }

        public override Status Process()
        {
            if (ProcessMethod == null) return Status.Failure;

            return ProcessMethod();
        }
    }

}