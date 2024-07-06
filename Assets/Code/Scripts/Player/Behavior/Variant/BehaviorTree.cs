using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree2 : MonoBehaviour
{
    public static BehaviorTree2 Instance;
    public Sim sim;
    private Node root;

    void Awake()
    {
        Instance = this;
        InitializeTree();
    }

    void InitializeTree()
    {
        root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new IsDayTime(),
                new Selector(new List<Node>
                {
                    // Thirsty
                    new Sequence(new List<Node>
                    {
                        new IsThirsty(),
                        new FindWater(),
                        new MoveToTarget(),
                        new ConsumeWater()
                    }),
                    // Hungry
                    new Sequence(new List<Node>
                    {
                        new IsHungry(),
                        new FindFood(),
                        new MoveToTarget(),
                        new ConsumeFood()
                    }),
                    // Work
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new IsBackpackFull(),
                            new GoToStorage(),
                            new DepositLogs()
                        }),
                        new Sequence(new List<Node>
                        {
                            new Selector(new List<Node>
                            {
                                new HasTarget(),
                                new FindNearestTree()
                            }),
                            new MoveToTree(),
                            new IsTreeActive(),
                            new ChopAction()
                        })
                    })
                })  
            }),
            new Sequence(new List<Node>
            {
                new IsNightTime(),
                new Selector(new List<Node>
                {
                    // Sleep
                    new Sequence(new List<Node>
                    {
                        new HasLogsInBackpack(),
                        new GoToStorage(),
                        new DepositLogs()
                    }),
                    new GoToSleep()
                })
            })
        });
    }

    public void Update()
    {
        root.Execute();
    }
}
