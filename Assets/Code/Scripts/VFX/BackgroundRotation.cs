using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(0.1f, 10f)] float rotationSpeed = 1f;

    [Header("References")]
    [SerializeField] Transform background;

    void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        background.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
