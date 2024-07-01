using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask layersToDamage;
    [SerializeField] int health = 3;

    public static event Action Hit;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.layer);

        if (layersToDamage == (layersToDamage | (1 << collision.gameObject.layer)))
        {
            Debug.Log("HIT");

            health--;

            Hit?.Invoke();

            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
