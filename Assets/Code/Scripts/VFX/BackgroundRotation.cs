using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(0.1f, 10f)] float rotationSpeed = 1f;

    [Header("References")]
    [SerializeField] Transform background;
    [SerializeField] Transform background2;

    void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        background.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime);
        background2.Rotate(0f, 0f, -rotationSpeed * Time.fixedDeltaTime);
    }
}
