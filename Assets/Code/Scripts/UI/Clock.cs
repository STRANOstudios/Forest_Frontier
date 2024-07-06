using UnityEngine;

[DisallowMultipleComponent]
public class Clock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform hourHand;
    [SerializeField] Transform minuteHand;

    private void OnEnable()
    {
        TimeManager.OnTimeChange += Change;
    }

    private void OnDisable()
    {
        TimeManager.OnTimeChange -= Change;
    }

    void Change(float value)
    {
        float hours = Mathf.Floor(value % 24);
        float minutes = Mathf.Floor((value % 1) * 60);

        float hourAngle = (hours % 12) * 30f; // 360 degrees / 12 hours = 30 degrees per hour
        float minuteAngle = minutes * 6f; // 360 degrees / 60 minutes = 6 degrees per minute

        // Set rotations
        hourHand.localRotation = Quaternion.Euler(0f, 0f, -hourAngle);
        minuteHand.localRotation = Quaternion.Euler(0f, 0f, -minuteAngle);
    }
}
