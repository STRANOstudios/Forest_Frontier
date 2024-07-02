using UnityEngine;
using UnityEngine.AI;

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

    public bool IsAtTarget()
    {
        return target != null && !agent.pathPending && agent.remainingDistance < distanceToStop;
    }

    public void ChopTree()
    {
        // Reduce hunger and thirst
        hunger -= Random.Range(1, 5);
        thirst -= Random.Range(1, 5);

        // Check if there is a valid target
        if (target != null)
        {
            // Rotate axe (assuming the axe rotates from 0 to 90 degrees)
            const float rotationSpeed = 90f; // Degrees per second
            float rotationAngle = Mathf.Min(rotationSpeed * Time.deltaTime, 90f); // Limit rotation angle per frame
            axe.Rotate(Vector3.right * rotationAngle);

            // Check if the axe has completed its rotation (reached 90 degrees)
            if (axe.localRotation.eulerAngles.x >= 90f)
            {
                // Simulate chopping action
                backpack.AddLog();
                target.GetComponent<Health>().TakeDamage();

                // Check if target is no longer active
                if (!target.activeSelf)
                {
                    target = null; // Clear target if it's no longer active
                    axe.localRotation = Quaternion.identity; // Reset axe rotation to original position
                }
            }
        }
    }


    public bool IsTreeActive()
    {
        return target != null && target.activeInHierarchy;
    }

    public bool IsBackpackFull()
    {
        return backpack.IsFull();
    }

    public void GoToStorage()
    {
        agent.SetDestination(storageLocation.position);
    }

    public void DepositLogs()
    {
        if (IsAtTarget())
        {
            backpack.RemoveLogs();
        }
    }

    public void GoToSleep()
    {
        agent.SetDestination(sleepLocation.position);
    }

    public bool HasLogsInBackpack()
    {
        return backpack.HasLogs();
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
