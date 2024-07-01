using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
public class DaylightManager : MonoBehaviour
{
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
        // Calculate light color and rotation based on time of day
        Color lightColor = CalculateLightColor(time);
        float lightRotation = CalculateLightRotation(time);

        // Tween color and rotation using DOTween
        sunLight.DOColor(lightColor, 1f); // Example duration of 1 second
        sunLight.transform.DORotate(new Vector3(lightRotation, -30, 0f), 1f); // Example duration of 1 second
    }

    private Color CalculateLightColor(float time)
    {
        // Example implementation: change color based on time of day
        if (time >= 6f && time < 18f)
        {
            // Daytime color
            return Color.white;
        }
        else
        {
            // Nighttime color
            return Color.blue;
        }
    }

    private float CalculateLightRotation(float time)
    {
        // Example implementation: change rotation based on time of day
        // Assume the sun rises from the horizon (0 degrees) to directly above (90 degrees) and sets back down
        float normalizedTime = time / 24f; // Normalize time between 0 and 1
        float rotationAngle = Mathf.Lerp(-90f, 90f, normalizedTime); // Example range of rotation from -90 to 90 degrees

        return rotationAngle;
    }
}
