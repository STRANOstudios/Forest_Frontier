using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Backpack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<GameObject> Logs = new();
    private List<GameObject> LogsActiveList = new();

    private int index = 0;

    public void AddLog()
    {
        index = Mathf.Clamp(index, 0, Logs.Count - 1);

        Logs[index].SetActive(true);
        LogsActiveList.Add(Logs[index]);
        index++;
    }

    public void RemoveLogs()
    {
        foreach (GameObject log in LogsActiveList)
        {
            log.SetActive(false);
        }

        LogsActiveList.Clear();
        index = 0;
    }

    public bool IsEmpty()
    {
        return LogsActiveList.Count == 0;
    }

    public bool IsFull()
    {
        return LogsActiveList.Count == Logs.Count;
    }
}
