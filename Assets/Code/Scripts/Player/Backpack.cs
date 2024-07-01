using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Backpack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<GameObject> Logs = new();

    private List<GameObject> LogsInactiveList = new();
    private List<GameObject> LogsActiveList = new();

    private void Awake()
    {
        LogsInactiveList = Logs;
    }

    private void OnEnable()
    {
        Health.Hit += AddLog;
        //in warehouse removelog
    }

    private void OnDisable()
    {
        Health.Hit -= AddLog;
    }

    private void AddLog()
    {
        LogsActiveList.Add(LogsInactiveList[0]);

        LogsInactiveList[0].SetActive(true);

        LogsInactiveList.RemoveAt(0);
    }

    private void RemoveLog()
    {
        LogsActiveList.Clear();
        LogsInactiveList.Clear();

        LogsInactiveList = Logs;

        foreach (GameObject log in Logs)
        {
            log.SetActive(false);
        }
    }
}
