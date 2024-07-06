using System.Collections;
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
    private Animator axeAnimator;

    private bool axeRotated = false;
    private Coroutine choppingCoroutine;
    private int hungerBackup;
    private int thirstBackup;

    //flags
    private bool isEating = false;
    private bool isDrinking = false;

    public delegate void SimDelegate(int value);
    public static SimDelegate OnHungerChanged;
    public static SimDelegate OnThirstChanged;

    private void Awake()
    {
        hungerBackup = hunger;
        thirstBackup = hunger;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        backpack = GetComponent<Backpack>();
        axeAnimator = GetComponent<Animator>();
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
        if (hunger < hungerLimit) isEating = true;
        else if (hunger == hungerBackup) isEating = false;
        return isEating;
    }

    public bool IsThirsty()
    {
        if (thirst < thirstLimit) isDrinking = true;
        else if (thirst == thirstBackup) isDrinking = false;
        return isDrinking;
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
        return !agent.pathPending && agent.remainingDistance <= distanceToStop && !agent.hasPath;
    }

    public void ChopTree()
    {
        choppingCoroutine ??= StartCoroutine(ChopTreeCoroutine());
    }

    private IEnumerator ChopTreeCoroutine()
    {
        // Check if the backpack is full
        if (backpack.IsFull())
        {
            choppingCoroutine = null;
            yield break;
        }

        if (target != null && IsAtTargetAgent())
        {
            // Reduce hunger and thirst
            hunger -= Random.Range(1, 5);
            thirst -= Random.Range(1, 5);

            OnHungerChanged?.Invoke(hunger);
            OnThirstChanged?.Invoke(thirst);

            if (target != null && axeAnimator != null)
            {
                // Rotation player's head
                gameObject.transform.rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position);

                axeAnimator.CrossFade("Swing", 0.1f);

                AnimatorStateInfo stateInfo = axeAnimator.GetCurrentAnimatorStateInfo(0);
                float chopDelay = stateInfo.length;

                // Wait for the duration of the animation
                yield return new WaitForSeconds(chopDelay);

                backpack.AddLog();
                target.GetComponent<Health>().TakeDamage();

                if (!target.activeSelf)
                {
                    target = null;
                    choppingCoroutine = null;
                    yield break;
                }
            }
            else yield return null;
        }

        axeAnimator.CrossFade("Idle", 0.1f);

        choppingCoroutine = null;
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
        hunger += target.GetComponentInParent<IEdibleDrinkable>().Energy;

        hunger = Mathf.Clamp(hunger, 0, hungerBackup);

        OnHungerChanged?.Invoke(hunger);

        ObjectPoolerManager.ReturnObjectToPool(target);
        target.SetActive(false);

        if (target != null) target = null;
    }

    public void ConsumeWater()
    {
        thirst += target.GetComponent<IEdibleDrinkable>().Energy;

        thirst = Mathf.Clamp(thirst, 0, thirstBackup);

        OnThirstChanged?.Invoke(thirst);

        if (target != null) target = null;
    }
}
