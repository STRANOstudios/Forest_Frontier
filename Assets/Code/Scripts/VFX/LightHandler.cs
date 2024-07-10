using UnityEngine;

[DisallowMultipleComponent]
public class LightHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(1, 24)] private int tunrOnTime;
    [SerializeField, Range(1, 24)] private int tunrOffTime;

    [Header("References")]
    [SerializeField] private Light[] lights;

    private bool IsNight = false;

    private void OnEnable()
    {
        TimeManager.OnTimeChange += ToggleLights;
    }

    private void OnDisable()
    {
        TimeManager.OnTimeChange -= ToggleLights;
    }

    public void ToggleLights(float time)
    {
        IsNight = time >= tunrOnTime || time < tunrOffTime;

        foreach (var light in lights)
        {
            light.enabled = IsNight;
        }
    }
}
