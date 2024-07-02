using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    [DisallowMultipleComponent]
    public class SimBehavior : MonoBehaviour
    {
        BehaviorTree tree;
        NavMeshAgent agent;

        public void Start ()
        {
            agent = GetComponent<NavMeshAgent>();

            tree = new BehaviorTree();

            Node getItem = new("Get Item");
            Node goToItemed = new("Go To Itemed");
            Node escape = new("Escape");

            tree.AddChild(getItem);
            getItem.AddChild(goToItemed);
            getItem.AddChild(escape);

        }
    }

}