using UnityEngine;

[DisallowMultipleComponent]
public class Food : MonoBehaviour, IEdibleDrinkable
{
    [Header("Settings")]
    [SerializeField, Min(0)] int _energy = 1;

    public int Energy => _energy;
}
