using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
public class DaylightManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(1, 24)] private float sunset = 19f;
    [SerializeField, Range(1, 24)] private float sunrise = 6f;

    public float time;

    private Light sunLight;

    private void Awake()
    {
        sunLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        TimeManager.OnTimeChange += UpdateLight;
    }

    private void OnDisable()
    {
        TimeManager.OnTimeChange -= UpdateLight;
    }

    private void UpdateLight(float time)
    {
        this.time = time;

        // Calculate light color and rotation based on time of day
        Color lightColor = CalculateLightColor(time);
        float lightRotation = CalculateLightRotation(time);

        // Tween color and rotation using DOTween
        sunLight.DOColor(lightColor, 5f).SetUpdate(true);
        sunLight.transform.DORotate(new Vector3(lightRotation, -30, 0f), 5f).SetUpdate(true);
    }

    private Color CalculateLightColor(float time)
    {
        // Example implementation: change color based on time of day
        if (time >= sunrise && time < sunset)
        {
            // Daytime color (white)
            return Color.white;
        }
        else
        {
            // Nighttime color (blue)
            return Color.blue;
        }
    }

    private float CalculateLightRotation(float time)
    {
        // Normalize time within a 24-hour format
        float normalizedTime = time / 24f;
        float rotationAngle = Mathf.Lerp(0f, 180f, normalizedTime);

        return rotationAngle;
    }
}
