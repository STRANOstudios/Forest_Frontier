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

    [Header("Debug")]
    [SerializeField, Range(0, 90), Tooltip("[Use only for testing]\nHigher levels correspond to increased bugs.")] private int timeSpeed = 0;

    public static float time = 0;

    private float currentTime;
    private int dayCount;

    public static event Action<float> OnTimeChange;
    public static event Action<int> OnDayChange;

    private void Awake()
    {
        time = currentTime = initialTime;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        UpdateTime();
    }

    private void UpdateTime()
    {
        currentTime += (Time.deltaTime / dayDuration) * 24f;

        if (currentTime >= 24f)
        {
            dayCount++;
            currentTime = 0f;
            OnDayChange?.Invoke(dayCount);
        }

        Time.timeScale = (currentTime >= dayTime && currentTime < nightTime)
                         ? dayTimeSpeed + timeSpeed
                         : nightTimeSpeed + timeSpeed;

        time = currentTime;

        OnTimeChange?.Invoke(currentTime);
    }
}
