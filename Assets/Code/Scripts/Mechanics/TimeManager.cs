using UnityEngine;

[DisallowMultipleComponent]
public class TimeManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Min(60), Tooltip("Duration of the day in real seconds")] private float dayDuration = 120f;

    [SerializeField, Range(1, 10)] private float dayTimeSpeed = 5f;
    [SerializeField, Range(1, 10)] private float nightTimeSpeed = 5f;

    [SerializeField, Range(1, 24)] private float dayTime = 6f;
    [SerializeField, Range(1, 24)] private float nightTime = 18f;

    public static float DayDuration;
    private float time = 0;

    private float hour = 0;

    public delegate void TimeEvent(float value);
    public static TimeEvent OnTimeChange;

    private void Awake()
    {
        DayDuration = dayDuration;
        hour = dayDuration / 24f;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        time += (Time.deltaTime / dayDuration) * 24f;

        if (time >= 24f) time = 0f;

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
