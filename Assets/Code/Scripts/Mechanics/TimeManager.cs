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
    public static float Time = 0;

    private float hour = 0;

    private void Awake()
    {
        DayDuration = dayDuration;
        hour = dayDuration / 24f;
    }

    private void Update()
    {
        Time += UnityEngine.Time.deltaTime * dayTimeSpeed;

        if (Time > dayDuration) Time = 0f;

        if (Time > hour * dayTime && Time < hour * nightTime)
        {
            UnityEngine.Time.timeScale = dayTimeSpeed;
            Debug.Log("Day time");
        }
        else if (Time < hour * dayTime)
        {
            UnityEngine.Time.timeScale = nightTimeSpeed;
            Debug.Log("Night time");
        }
    }
}
