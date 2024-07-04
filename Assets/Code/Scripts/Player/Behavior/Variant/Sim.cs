using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[DisallowMultipleComponent, RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Backpack))]
public class Sim : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Min(1)] int hunger = 100;
    [SerializeField, Min(1)] int thirst = 100;
    [Space]
    [SerializeField, Min(1)] int hungerLimit = 20;
    [SerializeField, Min(1)] int thirstLimit = 20;
    [Space]
    [SerializeField, Range(1, 24)] int timeToWork = 7;
    [SerializeField, Range(1, 24)] int timeToSleep = 18;
    [Space]
    [SerializeField, Range(0, 5)] float distanceToStop = 0;

    [Header("References")]
    [SerializeField] Transform axe;
    [Space]
    [SerializeField] Transform storageLocation;
    [SerializeField] Transform sleepLocation;
    [Space]
    [SerializeField] LayerMask treeLayer;
    [SerializeField] LayerMask foodLayer;
    [SerializeField] LayerMask waterLayer;

    private NavMeshAgent agent;
    private GameObject target;
    private Backpack backpack;

    private bool axeRotated = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        backpack = GetComponent<Backpack>();
    }

    void Update()
    {
        // Call behavior tree update
        BehaviorTree2.Instance.Update();
    }

    public bool IsDayTime()
    {
        return TimeManager.time >= timeToWork && TimeManager.time < timeToSleep;
    }

    public bool IsNightTime()
    {
        return TimeManager.time < timeToWork || TimeManager.time >= timeToSleep;
    }

    public bool IsHungry()
    {
        return hunger < hungerLimit;
    }

    public bool IsThirsty()
    {
        return thirst < thirstLimit;
    }

    public bool HasTarget()
    {
        return target != null;
    }

    public void FindNearestTree()
    {
        Collider[] trees = Physics.OverlapSphere(transform.position, 100f, treeLayer);
        float closestDistance = Mathf.Infinity;

        foreach (Collider tree in trees)
        {
            float distance = Vector3.Distance(transform.position, tree.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = tree.gameObject;
            }
        }
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    /// <summary>
    /// Checks if the agent has reached the target.
    /// </summary>
    /// <returns></returns>
    public bool IsAtTarget()
    {
        return target != null && !agent.pathPending && agent.remainingDistance < distanceToStop;
    }

    public bool IsAtTargetAgent()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath;
    }

    public void ChopTree()
    {
        // Reduce hunger and thirst
        hunger -= Random.Range(1, 5);
        thirst -= Random.Range(1, 5);

        if (target != null)
        {
            if (!axeRotated)
            {
                // Simulate axe swinging
                axe.Rotate(Vector3.right * 90);
                axeRotated = true;
            }
            else
            {
                // Reset axe rotation after swinging
                axe.Rotate(Vector3.left * 90);
                axeRotated = false;

                // Simulate chopping action with delay
                backpack.AddLog();
                target.GetComponent<Health>().TakeDamage();

                // Check if the target is still active
                if (!target.activeSelf)
                {
                    target = null;
                }
            }
        }
    }

    public bool IsTreeActive()
    {
        return target != null && target.activeSelf;
    }

    public void GoToStorage()
    {
        agent.SetDestination(storageLocation.position);
    }

    public void DepositLogs()
    {
        if (IsAtTargetAgent() && HasLogsInBackpack())
        {
            backpack.RemoveLogs();
        }
    }

    public bool IsBackpackFull()
    {
        return backpack.IsFull();
    }

    public bool HasLogsInBackpack()
    {
        return !backpack.IsEmpty();
    }

    public void GoToSleep()
    {
        agent.SetDestination(sleepLocation.position);
    }

    public void FindNearestFood()
    {
        Collider[] foods = Physics.OverlapSphere(transform.position, 100f, foodLayer);
        float closestDistance = Mathf.Infinity;

        foreach (Collider food in foods)
        {
            float distance = Vector3.Distance(transform.position, food.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = food.gameObject;
            }
        }
    }

    public void FindNearestWater()
    {
        Collider[] waters = Physics.OverlapSphere(transform.position, 100f, waterLayer);
        float closestDistance = Mathf.Infinity;

        foreach (Collider water in waters)
        {
            float distance = Vector3.Distance(transform.position, water.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = water.gameObject;
            }
        }
    }

    public void ConsumeFood()
    {
        hunger = 100;
        if (target != null)
        {
            target = null;
        }
    }

    public void ConsumeWater()
    {
        thirst = 100;
        if (target != null)
        {
            target = null;
        }
    }
}
