using System;
using UnityEngine;

[DisallowMultipleComponent]
public class TimeManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Min(60), Tooltip("Duration of the day in real seconds")] private float dayDuration = 120f;
    [Space]
    [SerializeField, Range(1, 10)] private float dayTimeSpeed = 5f;
    [SerializeField, Range(1, 10)] private float nightTimeSpeed = 5f;

    [SerializeField, Range(1, 24)] private float dayTime = 6f;
    [SerializeField, Range(1, 24)] private float nightTime = 18f;
    [Space]
    [SerializeField, Range(1, 24), Tooltip("Initial time in game")] private float initialTime = 7f;

    public static float time = 0;

    private int day = 0;

    public delegate void TimeEvent(float value);
    public static TimeEvent OnTimeChange;

    private void Awake()
    {
        time = initialTime;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        time += (Time.deltaTime / dayDuration) * 24f;

        if (time >= 24f)
        {
            day++;
            time = 0f;
        }

        if (time >= dayTime && time < nightTime)
        {
            Time.timeScale = dayTimeSpeed;
        }
        else
        {
            Time.timeScale = nightTimeSpeed;
        }

        OnTimeChange?.Invoke(time);
    }
}
